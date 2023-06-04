using SimpleShoppingCart.Domains.Base;

namespace SimpleShoppingCart.Domains
{
    /// <summary>
    /// Value Object. Shown as a record added in shopping cart.
    /// </summary>
    public class CartItem : SelfValidateDomainModel
    {
        public string SKU { get; private set; }
        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public double UnitPrice { get; private set; }
        public Dictionary<string, string> Options { get; private set; }
        public double SubTotalPrice { get; private set; }

        public CartItem(string sku, string name, int quantity, double unitPrice, Dictionary<string, string> options): base(sku, name, quantity, unitPrice)
        {
            Validate(sku, name, quantity, unitPrice);

            SKU = sku;
            Name = name;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Options = options;
            SubTotalPrice = quantity * unitPrice;
        }

        public CartItem(string sku, string name, int quantity, double unitPrice) : this(sku, name, quantity, unitPrice, new Dictionary<string, string>()) { }

        protected override void Validate(params object[] list)
        {
            if (string.IsNullOrWhiteSpace((string)list[0]))
            {
                throw new ArgumentException("SKU cannot be null or empty");
            }

            if (string.IsNullOrWhiteSpace((string)list[1]))
            {
                throw new ArgumentException("Item name cannot be null or empty");
            }

            if ((int)list[2] <= 0)
            {
                throw new ArgumentException("Quantity cannot be less than 1");
            }

            if ((double)list[3] < 0)
            {
                throw new ArgumentException("Unit price cannot be less than 0");
            }
        }
    }
}
