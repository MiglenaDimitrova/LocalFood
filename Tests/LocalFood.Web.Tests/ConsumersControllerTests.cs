namespace LocalFood.Web.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using LocalFood.Web.Controllers;
    using LocalFood.Web.Tests.Mock;
    using LocalFood.Web.ViewModels.Producers;
    using LocalFood.Web.ViewModels.Products;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class ConsumersControllerTests
    {
        //[Fact]
        //public void FavoritesShouldReturnViewWithCorrectModel()
        //{
        //    MyController<ConsumersController>
        //         .Instance(c => c.WithData(GlobalMocking.GetFakeProduct())
        //         .WithUser(GlobalMocking.GetFakeUser().Id))
        //         .Calling(c => c.Favorites(1))
        //         .ShouldReturn()
        //         .View(view => view.WithModelOfType<ProductsListViewModel>());
        //}

        // Routing
        [Fact]
        public void FavoritesActionShoudMatchSpecificRoute()
        {
            MyRouting
            .Configuration()
            .ShouldMap("/Consumers/Favorites?id=1")
            .To<ConsumersController>(c => c.Favorites(1));
        }
    }
}
