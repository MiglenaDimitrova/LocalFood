namespace LocalFood.Web.Tests
{
    using LocalFood.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class ConsumersControllerTests
    {
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
