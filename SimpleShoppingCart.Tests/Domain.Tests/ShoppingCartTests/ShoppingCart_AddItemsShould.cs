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
            var actual = mockShoppingCart.TotalCartPrice;

            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow(3, 20)]
        [DataRow(6, 40)]
        [DataRow(7, 50)]
        [DataRow(9, 60)]
        public void Buy3Jeans_ShouldApplyDiscountWithPriceOf2(int numberOfJeans, double expected)
        {
            var mockShoppingCart = CreateMockShoppingCart();
            var mockCartItems = new List<CartItem>
            {
                new CartItem("1", "Jeans", numberOfJeans, 10)
            };
            var mockCoupons = new List<Coupon>
            {
                new Coupon("jeans", 3, 1)
            };

            mockShoppingCart.AddItems(mockCartItems);
            mockShoppingCart.AddCoupon(mockCoupons);
            var actual = mockShoppingCart.TotalPrice;

            Assert.AreEqual(expected, actual);

        }

        [DataTestMethod]
        [DataRow(1, 1, 30)]
        [DataRow(2, 2, 45)]
        [DataRow(3, 3, 75)]
        [DataRow(4, 4, 90)]
        [DataRow(2, 1, 50)]
        public void BySetsOfTShirtAndJeans_ShouldApplyDiscountToSecondSet(int numberOfJeans, int numberOfTShirt, double expected)
        {
            var mockShoppingCart = CreateMockShoppingCart();
            var mockCartItems = new List<CartItem>
            {
                new CartItem("1", "Jeans", numberOfJeans, 20),
                new CartItem("1", "Tshirt", numberOfTShirt, 10)
            };
            var mockCoupon = new SetDiscountCoupon(2, 0.25);
            mockCoupon.SetDiscountItemSet("jeans");
            mockCoupon.SetDiscountItemSet("tshirt");
            var mockCoupons = new List<SetDiscountCoupon>
            {
                mockCoupon
            };

            mockShoppingCart.AddCoupon(mockCoupons);
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
