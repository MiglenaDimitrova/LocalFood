namespace LocalFood.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using LocalFood.Data.Common.Repositories;
    using LocalFood.Data.Models;

    public class VotesService : IVotesService
    {
        private readonly IRepository<Vote> votesRepository;

        public VotesService(IRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public double GetAverageVote(int producerId)
        {
            return this.votesRepository.All()
                 .Where(x => x.ProducerId == producerId)
                 .Select(x => (double)x.Value).Average();
        }

        public async Task SetVoteAsync(int producerId, string userId, byte value)
        {
            var vote = this.votesRepository.All()
                .Where(x => x.ProducerId == producerId && x.ApplicationUserId == userId).FirstOrDefault();
            if (vote == null)
            {
                vote = new Vote
                {
                    ApplicationUserId = userId,
                    ProducerId = producerId,
                };
                await this.votesRepository.AddAsync(vote);
            }

            vote.Value = value;
            await this.votesRepository.SaveChangesAsync();
        }
    }
}
