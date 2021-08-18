namespace LocalFood.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using LocalFood.Data.Common.Repositories;
    using LocalFood.Data.Models;
    using Moq;
    using Xunit;

    public class VotesServiceTests
    {
        [Fact]
        public async Task IfUserVotesTwiceOnlyOneVoteCounts()
        {
            List<Vote> list = new();
            Mock<IRepository<Vote>> mockRepoVotes = new();
            Mock<IDeletableEntityRepository<Producer>> mockRepoProducers = new();
            mockRepoVotes.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepoVotes.Setup(x => x.AddAsync(It.IsAny<Vote>()))
                .Callback((Vote vote) => list.Add(vote));

            var service = new VotesService(mockRepoVotes.Object, mockRepoProducers.Object);

            await service.SetVoteAsync(1, "abv", 1);
            await service.SetVoteAsync(1, "abv", 4);
            int expected = list.Count;
            Assert.Equal(1, expected);
            Assert.Equal(4, list.First().Value);
        }

        [Fact]
        public async Task AverageVoteShouldWorkCorrectly()
        {
            List<Vote> list = new();
            Mock<IRepository<Vote>> mockRepoVotes = new();
            Mock<IDeletableEntityRepository<Producer>> mockRepoProducers = new();
            mockRepoVotes.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepoVotes.Setup(x => x.AddAsync(It.IsAny<Vote>()))
                .Callback((Vote vote) => list.Add(vote));

            var service = new VotesService(mockRepoVotes.Object, mockRepoProducers.Object);

            await service.SetVoteAsync(1, "abv", 1);
            await service.SetVoteAsync(1, "abvg", 3);
            await service.SetVoteAsync(1, "abvgd", 5);

            var result = service.GetAverageVote(1);

            Assert.Equal(3.00, result);
        }
    }
}
