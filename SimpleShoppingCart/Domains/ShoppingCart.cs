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

        public double TotalPrice { get; private set; }

        /// <summary>
        /// Initializes a new instance of <see cref="ShoppingCart"/> class.
        /// </summary>
        /// <param name="customerReferenceId"></param>
        public ShoppingCart(string customerReferenceId) : base(customerReferenceId)
        {
            CustomerReferenceId = customerReferenceId;
            Items = new List<CartItem>();
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
                }
            }
            
            return TotalPrice;
        } 

        private void CalculateTotalPrice()
        {
            //Set total price to prevent recalculation when querying
            TotalPrice = Items.Sum(item => item.SubTotalPrice);
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
