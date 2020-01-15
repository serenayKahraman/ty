namespace Trendyol.ShoppingCart.Models
{
    public class Category : BaseModel
    {
        public Category(int id, int mainCategoryId, string title)
        {
            MainCategoryId = mainCategoryId;
            Title = title;
            Id = id;
        }
        public int MainCategoryId { get; }
        public string Title { get; }
    }
}
