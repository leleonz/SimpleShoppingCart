using SimpleShoppingCart.Domains;

namespace SimpleShoppingCart.Tests.Domain.Tests.ShoppingCartTests
{
    [TestClass]
    public class ShoppingCart_AddItemsShould
    {
        private const string FakeCustomerReferenceId = "TestCustomer";

        [DataTestMethod]
        [DataRow(1, 1)]
        [DataRow(2, 2)]
        [DataRow(3, 3)]
        public void AddItems_MultipleValidItemList_ShouldHaveSameCount(int requestedValidItems, int expected)
        {
            var mockShoppingCart = CreateMockShoppingCart();
            var mockCartItems = CreateMockCartItems(requestedValidItems);

            mockShoppingCart.AddItems(mockCartItems);
            var actual = mockShoppingCart.Items.Count;

            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow(1, 1, 1)]
        [DataRow(2, 1, 2)]
        [DataRow(1, 2, 1)]
        public void AddItems_MultipleValidItemWithNullList_ShouldExcludeNullFromCount(int requestedValidItems, int requestedNullItems, int expected)
        {
            var mockShoppingCart = CreateMockShoppingCart();
            var mockCartItems = CreateMockCartItems(requestedValidItems, requestedNullItems);

            mockShoppingCart.AddItems(mockCartItems);
            var actual = mockShoppingCart.Items.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddItems_NullItemList_ShouldDoNothing()
        {
            const int Expected = 0;
            var mockShoppingCart = CreateMockShoppingCart();

            mockShoppingCart.AddItems(null);
            var actual = mockShoppingCart.Items.Count;

            Assert.AreEqual(Expected, actual);
        }

        [TestMethod]
        public void AddItems_EmptyItemList_ShouldDoNothing()
        {
            const int Expected = 0;
            var mockShoppingCart = CreateMockShoppingCart();

            mockShoppingCart.AddItems(new List<CartItem>());
            var actual = mockShoppingCart.Items.Count;

            Assert.AreEqual(Expected, actual);
        }

        [DataTestMethod]
        [DataRow(1, 0, 10)]
        [DataRow(2, 1, 20)]
        [DataRow(3, 1, 30)]
        public void AddItems_AnyItemList_ShouldReturnCorrectTotalPrice(int requestedValidItems, int requestedNullItems, int expected)
        {
            var mockShoppingCart = CreateMockShoppingCart();
            var mockCartItems = CreateMockCartItems(requestedValidItems, requestedNullItems);

            mockShoppingCart.AddItems(mockCartItems);
            var actual = mockShoppingCart.TotalPrice;

            Assert.AreEqual(expected, actual);
        }

        private static ShoppingCart CreateMockShoppingCart()
        {
            return new ShoppingCart(FakeCustomerReferenceId);
        }

        private static IEnumerable<CartItem> CreateMockCartItems(int validItemCount, int nullItemCount = 0)
        {
            var mockItemList = new List<CartItem>();
            
            while (validItemCount > 0)
            {
                mockItemList.Add(new CartItem($"{validItemCount}", $"Item{validItemCount}", 1, 10));
                validItemCount--;
            }

            while (nullItemCount > 0)
            {
                mockItemList.Add(null);
                nullItemCount--;
            }

            return mockItemList;
        }
    }
}
