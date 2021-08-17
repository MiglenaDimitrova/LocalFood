namespace LocalFood.Web.Tests
{
    using System.Linq;
    using LocalFood.Data.Common.Repositories;
    using LocalFood.Data.Models;
    using LocalFood.Services.Data;
    using LocalFood.Web.Controllers;
    using LocalFood.Web.ViewModels.Producers;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class HomeControllerTests
    {
        [Fact]
        public void HomeControllerIndexActionShouldReturnView()
        {
            var controller = MyController<HomeController>.Instance();
            var call = controller.Calling(c => c.Index());
            call.ShouldReturn().View();
        }

        [Fact]
        public void HomeControllerErrorActionShouldReturnView()
        {
            var controller = MyController<HomeController>.Instance();
            var call = controller.Calling(c => c.Error());
            call.ShouldReturn().View();
        }

        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            Mock<IDeletableEntityRepository<Producer>> mockRepoProducers = new();
            Mock<IDeletableEntityRepository<Country>> mockRepoCountries = new();
            Mock<IDeletableEntityRepository<Location>> mockRepoLocation = new();
            Mock<IDeletableEntityRepository<Region>> mockRepoRegions = new();
            Mock<IDeletableEntityRepository<UserProducer>> mockRepoUserProducer = new();
            var service = new ProducersService(
                mockRepoProducers.Object,
                mockRepoRegions.Object,
                mockRepoUserProducer.Object,
                mockRepoCountries.Object,
                mockRepoLocation.Object);
            var controller = new HomeController(service);

            var result = controller.Index();

            Assert.NotNull(result);
            var viewRresult = Assert.IsType<ViewResult>(result);
            var model = viewRresult.Model;
            Assert.IsType<OurProducersViewModel>(model);
        }

        // Pipeline
        [Fact]
        public void IndexActionPipelineTest()
        {
            MyMvc
                .Pipeline()
                .ShouldMap("/Home/Index")
                .To<HomeController>(c => c.Index())
                .Which()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void ErrorActionPipelineTest()
        {
            MyMvc
                .Pipeline()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error())
                .Which()
                .ShouldReturn()
                .View();
        }
    }
}
