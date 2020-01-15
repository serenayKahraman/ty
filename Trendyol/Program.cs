using System;
using System.Collections.Generic;
using Trendyol.ShoppingCart.Implementation;
using Trendyol.ShoppingCart.Models;

namespace Trendyol.ShoppingCart
{
    public class Program
    {
        static double _costPerDelivery = 2;
        static double _costPerProduct = 1.2;
        static double _fixedCost = 2.99;

        static void Main(string[] args)
        {
            var category = new Category(1, 0, "foot");
            var cart = CreateShoppingCart(category);
            AddCampaigns(category, cart);
            AddCoupon(cart);
            var deliveryCostCalculator = new DeliveryCostCalculates(_costPerDelivery, _costPerProduct, _fixedCost);
            var delivery = deliveryCostCalculator.CalculateFor(cart);
            cart.Print();
            Console.WriteLine($"Delivery: {delivery}");
        }

        private static void AddCoupon(Implementation.ShoppingCart cart)
        {
            var coupon = new Coupon(100, 10, Campaign.DiscountType.Rate);
            cart.AddCoupon(coupon);
        }

        private static Implementation.ShoppingCart CreateShoppingCart(Category category)
        {
            var apple = new Product("apple", category, 100);
            var almond = new Product("almond", category, 150);

            var cart = new Implementation.ShoppingCart(new List<ShoppingCartItemDto>());
            cart.AddItem(apple, 3);
            cart.AddItem(almond, 1);
            return cart;
        }

        private static void AddCampaigns(Category category, Implementation.ShoppingCart cart)
        {
            var campaign1 = new Campaign(category, 20, 3, Campaign.DiscountType.Rate);
            var campaign2 = new Campaign(category, 50, 5, Campaign.DiscountType.Rate);
            var campaign3 = new Campaign(category, 3, 3, Campaign.DiscountType.Amount);
            cart.ApplyDiscounts(campaign1, campaign2, campaign3);
        }

    }
}
