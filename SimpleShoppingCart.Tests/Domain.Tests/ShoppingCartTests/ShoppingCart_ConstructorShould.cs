using SimpleShoppingCart.Domains;

namespace SimpleShoppingCart.Tests.Domain.Tests.ShoppingCartTests
{
    [TestClass]
    public class ShoppingCart_ConstructorShould
    {
        [TestMethod]
        public void Ctor_CustomerReferenceIdWithString_ShouldCreateObject()
        {
            const string CustomerReferenceId = "TestCustomer";
            var validShoppingCart = new ShoppingCart(CustomerReferenceId);

            Assert.IsNotNull(validShoppingCart);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_NullCustomerReferenceId_ShouldThrowArgumentException()
        {
            _ = new ShoppingCart(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_EmptyCustomerReferenceId_ShouldThrowArgumentException()
        {
            _ = new ShoppingCart("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_WhitespacesCustomerReferenceId_ShouldThrowArgumentException()
        {
            _ = new ShoppingCart("   ");
        }
    }
}
