namespace Trendyol.ShoppingCart.Models
{
    public class Campaign : BaseModel
    {

        public Campaign(Category category, double discount, int quantity, DiscountType discountType) 
        {
            Category = category;
            Discount = discount;
            DiscountT = discountType;
            Quantity = quantity;
        }
        public Category Category { get; }
        public double Discount { get; }
        public int Quantity { get; }
        public DiscountType DiscountT { get; }
        public enum DiscountType
        {
            Rate = 0,
            Amount = 1
        }

    }
}
