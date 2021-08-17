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
    using LocalFood.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class ProductsControllerTests
    {
        [Fact]
        public void AllShouldReturnViewWithCorrectModel()
        {
            MyController<ProductsController>
                .Instance(c => c.WithData(GlobalMocking.GetFakeProduct()))
                .Calling(c => c.All(1))
                .ShouldReturn()
                .View(view => view.WithModelOfType<ProductsListViewModel>());
        }

        [Fact]
        public void AddGetShouldReturnViewAndAlsoShouldHaveAuthorizeAttribute()
        {
            MyController<ProductsController>
                .Instance()
                .Calling(c => c.Add())
                .ShouldHave()
                .ActionAttributes(c => c.RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void AddPostShoulPostCorrectModelandShouldHaveAuthorizeAndPostAttribute()
        {
            MyController<ProductsController>
                .Instance()
                .WithUser(GlobalMocking.GetFakeUser().Id)
                .Calling(c => c.Add(GlobalMocking.GetFakeAddProductModel()))
                .ShouldHave()
                .ActionAttributes(c => c.RestrictingForAuthorizedRequests()
                .RestrictingForHttpMethod(HttpMethod.Post));
        }

        // Routing
        [Fact]
        public void AllActionShoudMatchSpecificRoute()
        {
            MyRouting
            .Configuration()
            .ShouldMap("/Products/All?id=1")
            .To<ProductsController>(c => c.All(1));
        }

        [Fact]
        public void AddActionShoudMatchSpecificRoute()
        {
            MyRouting
            .Configuration()
            .ShouldMap("/Products/Add")
            .To<ProductsController>(c => c.Add());
        }

        [Fact]
        public void MyProductsActionShoudMatchSpecificRoute()
        {
            MyRouting
            .Configuration()
            .ShouldMap("/Products/MyProducts?id=1")
            .To<ProductsController>(c => c.MyProducts(1));
        }

        [Fact]
        public void EditActionShoudMatchSpecificRoute()
        {
            MyRouting
            .Configuration()
            .ShouldMap("/Products/Edit?id=1")
            .To<ProductsController>(c => c.Edit(1));
        }

        [Fact]
        public void PostAddShouldMatchRoute()
        {
            MyRouting
                .Configuration()
                .ShouldMap(request => request
                .WithPath("/Products/Add")
                .WithMethod(HttpMethod.Post))
                .To<ProductsController>(c => c.Add(With.Any<ProductInputModel>()));
        }
    }
}
