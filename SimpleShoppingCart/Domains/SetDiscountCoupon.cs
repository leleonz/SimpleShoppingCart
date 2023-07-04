namespace SimpleShoppingCart.Domains
{
    public class SetDiscountCoupon : IDiscountableCoupon //inherit from SelfValidateDomainModel omitted
    {
        public IList<string> DiscountItemNames { get; private set; }
        //Customer have to buy preset total quantity of all items listed in DiscountItemNames to be eligible
        public int BuyQuantity { get; private set; }

        public double DiscountPercentage { get; private set; }

        public SetDiscountCoupon(int buyQuantity, double discountPercentage)
        {
            DiscountItemNames = new List<string>();
            DiscountPercentage = discountPercentage;
            BuyQuantity = buyQuantity;
        }

        /// <summary>
        /// Setting item name(s) eligible for coupon
        /// </summary>
        /// <param name="itemName"></param>
        public void SetDiscountItemSet(string itemName)
        {
            DiscountItemNames.Add(itemName.ToLower());
        }

        public double CalculateDiscountedPrice(IList<CartItem> items)
        {
            var discountedAmount = 0.0;
            var eligibleDiscountItems = items.Where(item => DiscountItemNames.Contains(item.Name.ToLower()));

            if (eligibleDiscountItems.Any())
            {
                var minimumSet = 0;
                foreach (var itemName in DiscountItemNames)
                {
                    var quantity = eligibleDiscountItems.Where(e => e.Name.ToLower() == itemName).Sum(items => items.Quantity);
                    minimumSet = minimumSet == 0 ? quantity : Math.Min(minimumSet, quantity / BuyQuantity);
                }

                if (minimumSet > 0)
                {
                    foreach (var itemName in DiscountItemNames)
                    {
                        var item = eligibleDiscountItems.Where(e => e.Name.ToLower() == itemName).First();
                        discountedAmount += (BuyQuantity * item.UnitPrice * DiscountPercentage) * minimumSet;
                    }
                }
            }

            return discountedAmount;
        }
    }
}
