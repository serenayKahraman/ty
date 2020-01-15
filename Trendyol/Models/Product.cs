namespace Trendyol.ShoppingCart.Models
{
    public class Product : BaseModel
    {
        public Product(string name, Category category, double price)
        {
            Name = name;
            Category = category;
            Price = price;
        }
        public string Name { get; }
        public Category Category { get; }
        public double Price { get; }
    }
}
