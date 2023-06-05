using SimpleShoppingCart.Domains.Base;

namespace SimpleShoppingCart.Domains
{
    /// <summary>
    /// Value Object. Shown as a shopping cart record with only necessary information.
    /// </summary>
    public class CartItem : SelfValidateDomainModel
    {
        /// <summary>
        /// Mandatory. Unique id per item.
        /// </summary>
        public string SKU { get; private set; }

        /// <summary>
        /// Mandatory. Product name, e.g. T-shirt, Jeans
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Amount of item added. Must be more than 0.
        /// </summary>
        public int Quantity { get; private set; }

        /// <summary>
        /// Price per unit. Must be more than or equal 0 (gift).
        /// </summary>
        public double UnitPrice { get; private set; }

        /// <summary>
        /// Optional. For setting variation options, e.g. color : blue
        /// </summary>
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
            // Set subtotal during creation to prevent recalculation when querying
            SubTotalPrice = quantity * unitPrice;
        }

        /// <summary>
        /// Initializes new instance of <see cref="CartItem"/> class.
        /// </summary>
        /// <param name="sku"></param>
        /// <param name="name"></param>
        /// <param name="quantity"></param>
        /// <param name="unitPrice"></param>
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
