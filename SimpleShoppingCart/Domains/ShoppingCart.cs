using SimpleShoppingCart.Domains.Base;

namespace SimpleShoppingCart.Domains
{
    /// <summary>
    /// Aggregate root. Entity. Each customer have their own shopping cart.
    /// </summary>
    public class ShoppingCart : SelfValidateDomainModel
    {
        /// <summary>
        /// Surrogate key. Indicate shopping cart owner.
        /// </summary>
        public string CustomerReferenceId { get; private set; }

        public IList<CartItem> Items { get; private set; }

        public IList<IDiscountableCoupon> Coupons { get; private set; }

        public double TotalCartPrice { get; private set; }

        public double DiscountedPrice { get; private set; }

        public double TotalPrice { get; private set; }

        /// <summary>
        /// Initializes a new instance of <see cref="ShoppingCart"/> class.
        /// </summary>
        /// <param name="customerReferenceId"></param>
        public ShoppingCart(string customerReferenceId) : base(customerReferenceId)
        {
            CustomerReferenceId = customerReferenceId;
            Items = new List<CartItem>();
            Coupons = new List<IDiscountableCoupon>();
            TotalCartPrice = 0;
            DiscountedPrice = 0;
            TotalPrice = 0;
        }

        /// <summary>
        /// Add product(s) to cart.
        /// </summary>
        /// <param name="cartItems"></param>
        /// <returns></returns>
        public double AddItems(IEnumerable<CartItem> cartItems)
        {
            if (cartItems != null)
            {
                var validItems = cartItems.Where(item => item != null);

                if (validItems.Any())
                {
                    foreach (var item in validItems)
                    {
                        Items.Add(item);
                    }
                    CalculateTotalPrice();
                    CalculateDiscountedPrice();
                }
            }

            TotalPrice = TotalCartPrice - DiscountedPrice;

            return TotalPrice;
        } 

        public void AddCoupon(IEnumerable<IDiscountableCoupon> coupons)
        {
            foreach(var coupon in coupons)
            {
                Coupons.Add(coupon);
            }
        }

        /* Price calculation is assumed to be responsibility of shopping cart in this simple context.
           In the case of having possibility to change calculation formula, 
           calculate price method can be extracted to another Calculator interface, 
           Shopping cart model can then be constructed using factory together with chosen calculator class. */
        private void CalculateTotalPrice()
        {
            // Set total price to prevent recalculation when querying
            TotalCartPrice = Items.Sum(item => item.SubTotalPrice);
        }

        private void CalculateDiscountedPrice()
        {
            foreach (var coupon in Coupons)
            {
                DiscountedPrice += coupon.CalculateDiscountedPrice(Items);
            }
        }

        protected override void Validate(params object[] list)
        {
            if (string.IsNullOrWhiteSpace((string)list[0]))
            {
                throw new ArgumentException("Customer reference id cannot be null or empty");
            }
        }
    }
}
