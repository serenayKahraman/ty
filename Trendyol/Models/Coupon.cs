namespace Trendyol.ShoppingCart.Models
{
    public class Coupon
    {
        public Coupon(int minAmount, double discount, Campaign.DiscountType discountType)
        {
            MinAmount = minAmount;
            Discount = discount;
            DiscountType = discountType;
        }
        public int MinAmount { get; }
        public double Discount { get; set; }
        public Campaign.DiscountType DiscountType { get; }
    }
}
