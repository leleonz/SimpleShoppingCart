namespace SimpleShoppingCart.Domains
{
    public interface IDiscountableCoupon
    {
        /// <summary>
        /// Calculate discounted amount/ price based on condition set
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        double CalculateDiscountedPrice(IList<CartItem> items);
    }
}
