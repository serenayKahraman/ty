using System.Linq;

namespace Trendyol.ShoppingCart.Implementation
{
    public class DeliveryCostCalculates
    {
        public DeliveryCostCalculates(double conCostPerDelivery, double costPerProduct, double fixedCost)
        {
            CostPerDelivery = conCostPerDelivery;
            CostPerProduct = costPerProduct;
            FixedCost = fixedCost;
        }


        public double CostPerDelivery { get; }
        public double FixedCost { get; }
        public double CostPerProduct { get; }

        public double CalculateFor(ShoppingCart cart)
        {
            if (cart == null)
                return 0.0;
            var numberOfDeliveries = cart.Items.GroupBy(s => s.Product.Category).Count();
            var numberOfProduct = cart.Items.GroupBy(s => s.Product).Count();
            return (numberOfDeliveries * CostPerDelivery + numberOfProduct * CostPerProduct) + FixedCost;
        }
    }
}
