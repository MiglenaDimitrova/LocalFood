namespace LocalFood.Web.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using LocalFood.Data.Common.Repositories;
    using LocalFood.Data.Models;
    using LocalFood.Services.Data;
    using LocalFood.Web.Controllers;
    using LocalFood.Web.Tests.Mock;
    using LocalFood.Web.ViewModels.Markets;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class MarketsControllerTests
    {
        [Fact]
        public void MarketsAllShouldReturnViewWithCorrectModelMyTested()
        {
            MyController<MarketsController>
                .Instance(c => c.WithData(GlobalMocking.GetFakeMarket()))
                .Calling(c => c.All(1))
                .ShouldReturn()
                .View(view => view.WithModelOfType<MarketsListViewModel>());
        }

        [Fact]
        public void MarketsAllShouldReturnViewWithCorrectModel()
        {
            Mock<IDeletableEntityRepository<Market>> mockRepoMarkets = new();
            var service = new MarketsService(mockRepoMarkets.Object);

            var controller = new MarketsController(service);

            var result = controller.All();

            Assert.NotNull(result);
            var viewRresult = Assert.IsType<ViewResult>(result);
            var model = viewRresult.Model;
            Assert.IsType<MarketsListViewModel>(model);
        }

        // Routing
        [Fact]
        public void AllActionShoudMatchSpecificRoute()
        {
            MyRouting
            .Configuration()
            .ShouldMap("/Markets/All?id=1")
            .To<MarketsController>(c => c.All(1));
        }
    }
}
