namespace SimpleShoppingCart.Domains
{
    public class Coupon : IDiscountableCoupon //inherit from SelfValidateDomainModel omitted
    {
        public string DiscountItemName { get; private set; }
        public int BuyQuantity { get; private set; }
        public int FreeQuantity { get; private set; }

        public Coupon(string discountItemName, int buyQuantity, int freeQuantity)
        {
            DiscountItemName = discountItemName;
            BuyQuantity = buyQuantity;
            FreeQuantity = freeQuantity;
        }

        public double CalculateDiscountedPrice(IList<CartItem> items)
        {
            var eligibleDiscountItem = items.Where(item => item.Name.ToLower() == DiscountItemName.ToLower());

            if (eligibleDiscountItem.Any())
            {
                var discountItemCount = eligibleDiscountItem.Sum(item => item.Quantity);
                var unitPerCost = eligibleDiscountItem.First().UnitPrice;

                return discountItemCount / BuyQuantity * (unitPerCost * FreeQuantity);
            }

            return 0;
        }
    }
}
