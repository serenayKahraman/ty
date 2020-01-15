using System;
using System.Collections.Generic;
using System.Linq;
using Trendyol.ShoppingCart.Models;

namespace Trendyol.ShoppingCart.Implementation
{
    public class ShoppingCart
    {
        public ShoppingCart(IList<ShoppingCartItemDto> items)
        {
            Items = items;
        }

        public IList<ShoppingCartItemDto> Items { get; }
        public double CampaignDiscount { get; private set; }
        public double CouponDiscount { get; private set; }
        public double TotalAfterDiscountPrice => Items.Sum(s => s.Product.Price * s.Quantity) - CampaignDiscount - CouponDiscount;
        public double TotalPrice => Items.Sum(s => s.Product.Price * s.Quantity);

        public void AddItem(Product product, int count)
        {
            Items.Add(new ShoppingCartItemDto { Quantity = count, Product = product });
        }

        public void ApplyDiscounts(Campaign campaign1, Campaign campaign2, Campaign campaign3)
        {
            var discountList = new List<double>
                {CalculateDiscount(campaign1), CalculateDiscount(campaign2), CalculateDiscount(campaign3)};
            CampaignDiscount = discountList.OrderByDescending(s => s).First();
        }

        public void AddCoupon(Coupon coupon)
        {
            if (coupon.MinAmount <= TotalPrice)
                CouponDiscount = coupon.DiscountType == Campaign.DiscountType.Rate ? TotalPrice * coupon.Discount / 100
                    : coupon.Discount;
        }

        private double CalculateDiscount(Campaign campaign)
        {
            var price = Items.Where(s => s.Product.Category.Id == campaign.Category.Id).Sum(s => s.Product.Price * s.Quantity);
            if (price == 0)
                return 0;
            var categoryItemCount = Items.Count(s => s.Product.Category.Id == campaign.Category.Id);
            return (campaign.DiscountT == Campaign.DiscountType.Rate && categoryItemCount > campaign.Quantity) ?
                price * campaign.Discount / 100
                : campaign.Discount;
        }

        public void Print()
        {
            Items.GroupBy(s => s.Product.Category).ToList().ForEach(f =>
              {
                  Console.WriteLine($"Category Name: {f.Key.Title}");
                  Items.Where(t => t.Product.Category.Id == f.Key.Id).ToList().ForEach(p =>
                        {
                            Console.WriteLine($"Product Name: {p.Product.Name} || Unit Price: {p.Product.Price * p.Quantity} || Quantity: {p.Quantity}");
                        });
              });
            Console.WriteLine($"Total Price: {TotalPrice}");
            Console.WriteLine($"Total Discount : {CampaignDiscount + CouponDiscount}");
            Console.WriteLine($"Total After Discount Price : {TotalAfterDiscountPrice}");

        }
    }

    public class ShoppingCartItemDto
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }


}
