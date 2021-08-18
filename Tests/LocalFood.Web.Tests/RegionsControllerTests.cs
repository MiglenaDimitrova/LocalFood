namespace LocalFood.Web.Tests
{
    using System.Collections.Generic;
    using LocalFood.Web.Controllers;
    using LocalFood.Web.ViewModels.Producers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class RegionsControllerTests
    {
        [Fact]
        public void GetShouldReturnCorrectData()
        {
            MyController<RegionsController>
                .Instance()
                .Calling(c => c.Get(1))
                .ShouldReturn()
                .ResultOfType<List<RegionInputModel>>();
        }
    }
}
