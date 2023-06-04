namespace SimpleShoppingCart.Domains.Base
{
    public abstract class SelfValidateDomainModel
    {
        protected SelfValidateDomainModel(params object[] list)
        {
            Validate(list);
        }

        protected abstract void Validate(params object[] list);
    }
}
