﻿using InStock.Frontend.Abstraction.Services.Navigation;
using InStock.Frontend.Core.PageModels.Dashboard;
using InStock.Frontend.Core.PageModels.Inventory;
using Moq;

namespace InStock.Frontend.Tests.Core.UnitTests.PageModels.Dashboard
{
	public class MainPageModelTests
	{
        private readonly Mock<INavigationService> _navigationService;
        private readonly MainPageModel _pageModel;

        public MainPageModelTests()
		{
            _navigationService = new Mock<INavigationService>();
            _pageModel = new MainPageModel(_navigationService.Object);
		}

        [Test]
        public void Items_IsNotNull()
            => Assert.That(_pageModel.Items, Is.Not.Null);

        [Test]
        public void Items_Selection_InvokesNavigation()
        {
            _ = _navigationService.Setup(
                n => n.NavigateToAsync<InventoryPageModel>(
                    It.IsAny<object>(),
                    It.IsAny<bool>()))
                .Returns(Task.CompletedTask);

            _pageModel.Items!.First().Command.Execute(null);

            _navigationService.Verify(
                n => n.NavigateToAsync<InventoryPageModel>(
                    It.IsAny<object>(),
                    false),
                Times.Once);
        }
	}
}