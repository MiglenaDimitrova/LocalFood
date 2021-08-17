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
    using LocalFood.Web.ViewModels.Producers;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class ProducersControllerTests
    {
        [Fact]
        public void AllShouldReturnViewWithCorrectModelMyTested()
        {
            MyController<ProducersController>
                .Instance(c => c.WithData(GlobalMocking.GetFakeProducer()))
                .Calling(c => c.All(1))
                .ShouldReturn()
                .View(view => view.WithModelOfType<ProducersListViewModel>());
        }

        [Fact]
        public void CreateShouldReturnViewWithCorrectModel()
        {
            MyController<ProducersController>
                .Instance(c => c.WithData(GlobalMocking.GetFakeProducer()))
                .Calling(c => c.All(1))
                .ShouldReturn()
                .View(view => view.WithModelOfType<ProducersListViewModel>());
        }

        [Fact]
        public void CreateGetShouldReturnViewAndAlsoShouldHaveAuthorizeAttribute()
        {
            MyController<ProducersController>
                .Instance()
                .Calling(c => c.Create())
                .ShouldHave()
                .ActionAttributes(c => c.RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        //[Fact]
        //public void CreatePostShoulPostCorrectModelandShouldHaveAuthorizeAndPostAttribute()
        //{
        //    MyController<ProducersController>
        //        .Instance()
        //        .WithUser(GlobalMocking.GetFakeUser().Id)
        //        .Calling(c => c.Create(GlobalMocking.GetFakeCreateProducerModel()))
        //        .ShouldHave()
        //        .ActionAttributes(c => c.RestrictingForAuthorizedRequests()
        //        .RestrictingForHttpMethod(HttpMethod.Post));
        //}

        [Fact]
        public void AllShouldReturnViewWithCorrectModel()
        {
            var listProducers = new List<Producer>();
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
            mockRepoProducers.Setup(x => x.AddAsync(It.IsAny<Producer>()))
                .Callback((Producer producer) => listProducers.Add(producer));

            var controller = new ProducersController(service, null, null, null);
            mockRepoProducers.Object.AddAsync(new Producer());
            mockRepoProducers.Object.AddAsync(new Producer());
            mockRepoProducers.Object.AddAsync(new Producer());

            mockRepoProducers.Object.SaveChangesAsync();

            var result = controller.All();

            Assert.NotNull(result);
            var viewRresult = Assert.IsType<ViewResult>(result);
            var model = viewRresult.Model;
            var producersListViewModel = Assert.IsType<ProducersListViewModel>(model);
            Assert.IsType<ProducersListViewModel>(model);
            Assert.Equal(3, listProducers.Count);
        }

        // Routing
        [Fact]
        public void AllActionShoudMatchSpecificRoute()
        {
            MyRouting
            .Configuration()
            .ShouldMap("/Producers/All?id=1")
            .To<ProducersController>(c => c.All(1));
        }

        [Fact]
        public void CreateActionShoudMatchSpecificRoute()
        {
            MyRouting
            .Configuration()
            .ShouldMap("/Producers/Create")
            .To<ProducersController>(c => c.Create());
        }

        [Fact]
        public void MyProductsActionShoudMatchSpecificRoute()
        {
            MyRouting
            .Configuration()
            .ShouldMap("/Producers/MyProfile")
            .To<ProducersController>(c => c.MyProfile());
        }

        [Fact]
        public void EditActionShoudMatchSpecificRoute()
        {
            MyRouting
            .Configuration()
            .ShouldMap("/Producers/Edit?id=1")
            .To<ProducersController>(c => c.Edit(1));
        }
    }
}
