namespace SimpleShoppingCart.Domains.Base
{
    /// <summary>
    /// Enforce domain model to validate itself during creation.
    /// </summary>
    public abstract class SelfValidateDomainModel
    {
        protected SelfValidateDomainModel(params object[] list)
        {
            // Responsibility to ensure a valid domain model is within itself. See more in README.md
            Validate(list);
        }

        /// <summary>
        /// To include validation and throw domain related exceptions.
        /// </summary>
        /// <param name="list"></param>
        protected abstract void Validate(params object[] list);
    }
}
