using System.Collections.Generic;
using NUnit.Framework;
using Trendyol.ShoppingCart.Implementation;
using Trendyol.ShoppingCart.Models;

namespace ShoppingCart.Test
{
    public class Tests
    {
        static double _costPerDelivery = 2;
        static double _costPerProduct = 1.2;
        static double _fixedCost = 2.99;
        [Test]
        public void ShouldGetTotalAmountAfterDiscount()
        {
            var category = new Category(1, 0, "foot");
            var cart = CreateShoppingCart(category);
            var result = cart.TotalAfterDiscountPrice;
            Assert.AreEqual(result,450);
        }

        [Test]
        public void ShouldGetCouponDiscount()
        {
            var category = new Category(1, 0, "foot");
            var cart = CreateShoppingCart(category);
            AddCoupon(cart);
            var result = cart.CouponDiscount;
            Assert.AreEqual(result, 45);
        }


        [Test]
        public void ShouldDeliveryCost()
        {
            var category = new Category(1, 0, "foot");
            var cart = CreateShoppingCart(category);
            var deliveryCostCalculator = new DeliveryCostCalculates(_costPerDelivery, _costPerProduct, _fixedCost);
            var result = deliveryCostCalculator.CalculateFor(cart);
            Assert.AreEqual(result, 45);
        }


        #region  util
        private static void AddCoupon(Trendyol.ShoppingCart.Implementation.ShoppingCart cart)
        {
            var coupon = new Coupon(100, 10, Campaign.DiscountType.Rate);
            cart.AddCoupon(coupon);
        }

        private static Trendyol.ShoppingCart.Implementation.ShoppingCart CreateShoppingCart(Category category)
        {
            var apple = new Product("apple", category, 100);
            var almond = new Product("almond", category, 150);

            var cart = new Trendyol.ShoppingCart.Implementation.ShoppingCart(new List<ShoppingCartItemDto>());
            cart.AddItem(apple, 3);
            cart.AddItem(almond, 1);
            return cart;
        }

        private static void AddCampaigns(Category category, Trendyol.ShoppingCart.Implementation.ShoppingCart cart)
        {
            var campaign1 = new Campaign(category, 20, 3, Campaign.DiscountType.Rate);
            var campaign2 = new Campaign(category, 50, 5, Campaign.DiscountType.Rate);
            var campaign3 = new Campaign(category, 3, 3, Campaign.DiscountType.Amount);
            cart.ApplyDiscounts(campaign1, campaign2, campaign3);
        }


        #endregion
    }
}