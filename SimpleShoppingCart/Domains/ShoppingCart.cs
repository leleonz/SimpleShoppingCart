using SimpleShoppingCart.Domains.Base;

namespace SimpleShoppingCart.Domains
{
    public class ShoppingCart : SelfValidateDomainModel
    {
        public string CustomerReferenceId { get; private set; }
        public IList<CartItem> Items { get; private set; }
        public double TotalPrice { get; private set; }

        public ShoppingCart(string customerReferenceId) : base(customerReferenceId)
        {
            CustomerReferenceId = customerReferenceId;
            Items = new List<CartItem>();
            TotalPrice = 0;
        }

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
