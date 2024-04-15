using InStock.Common.InventoryService.Abstraction.Entities;
using InStock.Frontend.Abstraction.Adapters;
using InStock.Frontend.Abstraction.Managers;
using InStock.Frontend.Abstraction.Models;
using InStock.Frontend.Abstraction.Services.Navigation;
using InStock.Frontend.Core.PageModels.Dashboard;
using InStock.Frontend.Core.Services.Platform;
using Moq;

namespace InStock.Frontend.Tests.Core.UnitTests.PageModels.Dashboard
{
	public class MainPageModelTests
	{
        private Mock<INavigationService> _navigationService;
        private MainPageModel _pageModel;

        [SetUp]
        public void Setup()
		{
            _navigationService = new Mock<INavigationService>();

            var _imageService = new Mock<IImageService>();
            var locationManager = new Mock<ILocationsManager>();
            var revenueManager = new Mock<IRevenueManager>();
            var adapter = new Mock<IAdapter<IList<RevenueReport>, ChartDataSet>>();
            _pageModel = new MainPageModel(
                _navigationService.Object,
                _imageService.Object,
                locationManager.Object,
                revenueManager.Object,
                adapter.Object);
		}

        [Test]
        public void Items_IsNotNull()
            => Assert.That(_pageModel.Items, Is.Not.Null);

        [Test]
        public void HeaderViewModel_IsNotNull()
            => Assert.That(_pageModel.HeaderViewModel, Is.Not.Null);
	}
}