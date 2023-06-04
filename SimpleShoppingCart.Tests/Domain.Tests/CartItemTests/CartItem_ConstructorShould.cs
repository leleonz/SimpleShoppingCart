using SimpleShoppingCart.Domains;

namespace SimpleShoppingCart.Tests.Domain.Tests.CartItemTests
{
    [TestClass]
    public class CartItem_ConstructorShould
    {
        private const string FakeSKU = "TestSKU";
        private const string FakeName = "TestName";
        private const int FakeQuantity = 1;
        private const double FakeUnitPrice = 10;

        [TestMethod]
        public void Ctor_WithoutOptions_ShouldCreateObject()
        {
            var validCartItem = new CartItem(FakeSKU, FakeName, FakeQuantity, FakeUnitPrice);

            Assert.IsNotNull(validCartItem);
        }

        #region Test SKU
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_NullSKU_ShouldThrowArgumentException()
        {
            _ = new CartItem(null, FakeName, FakeQuantity, FakeUnitPrice);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_EmptySKU_ShouldThrowArgumentException()
        {
            _ = new CartItem("", FakeName, FakeQuantity, FakeUnitPrice);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_WhitespacesSKU_ShouldThrowArgumentException()
        {
            _ = new CartItem("   ", FakeName, FakeQuantity, FakeUnitPrice);
        }
        #endregion

        #region Test Item Name
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_NullName_ShouldThrowArgumentException()
        {
            _ = new CartItem(FakeSKU, null, FakeQuantity, FakeUnitPrice);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_EmptyName_ShouldThrowArgumentException()
        {
            _ = new CartItem(FakeName, "", FakeQuantity, FakeUnitPrice);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_WhitespacesName_ShouldThrowArgumentException()
        {
            _ = new CartItem(FakeSKU, "    ", FakeQuantity, FakeUnitPrice);
        }
        #endregion

        #region Test Quantity
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_ZeroQuantity_ShouldThrowArgumentException()
        {
            _ = new CartItem(FakeSKU, FakeName, 0, FakeUnitPrice);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_LessThanZeroQuantity_ShouldThrowArgumentException()
        {
            _ = new CartItem(FakeName, FakeName, -1, FakeUnitPrice);
        }
        #endregion

        #region Test Unit Price
        [TestMethod]
        public void Ctor_ZeroUnitPrice_ShouldCreateObject()
        {
            var validCartItem = new CartItem(FakeSKU, FakeName, FakeQuantity, 0);

            Assert.IsNotNull(validCartItem);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_NegativeUnitPrice_ShouldThrowArgumentException()
        {
            _ = new CartItem(FakeName, FakeName, FakeQuantity, -1);
        }
        #endregion

        [DataTestMethod]
        [DataRow(1, 10, 10)]
        [DataRow(2, 15, 30)]
        [DataRow(3, 20, 60)]
        public void Ctor_AnyQuantityWithUnitPrice_ShouldHaveCorrectSubTotalPrice(int quantity, double unitPrice, double expected)
        {
            var validCartItem = new CartItem(FakeSKU, FakeName, quantity, unitPrice);
            var actual = validCartItem.SubTotalPrice;

            Assert.AreEqual(expected, actual);
        }
    }
}
