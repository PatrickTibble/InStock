using InStock.Frontend.Abstraction.Models;
using InStock.Frontend.Core.PageModels.Inventory;

namespace InStock.Frontend.Tests.Core.UnitTests.PageModels.Inventory
{
	public class InventoryItemDetailsPageModelTests
	{
        private InventoryItemDetailsPageModel _pageModel;
        private InventoryItem _item;

        [SetUp]
        public void Setup()
        {
            _pageModel = new InventoryItemDetailsPageModel();
            _item = new InventoryItem()
            {
                Name = "Test",
                Description = "Test Description"
            };
        }

        [Test]
        public void ConfirmViewModel_Initializes()
            => Assert.That(_pageModel.ConfirmViewModel, Is.Not.Null);

        [Test]
        public void CancelViewModel_Initializes()
            => Assert.That(_pageModel.CancelViewModel, Is.Not.Null);

        [Test]
        public async Task NavigationData_Null_NameIsNull()
        {
            await _pageModel.InitializeAsync();
            Assert.That(_pageModel.Name, Is.Null);
        }

        [Test]
        public async Task NavigationData_Null_DescriptionIsNull()
        {
            await _pageModel.InitializeAsync();
            Assert.That(_pageModel.Description, Is.Null);
        }

        [Test]
        public async Task NavigationData_NotNull_NameIsNotNull()
        {
            await _pageModel.InitializeAsync(_item);
            Assert.That(_pageModel.Name, Is.Not.Null);
        }

        [Test]
        public async Task NavigationData_NotNull_DescriptionIsNotNull()
        {
            await _pageModel.InitializeAsync(_item);
            Assert.That(_pageModel.Description, Is.Not.Null);
        }
    }
}