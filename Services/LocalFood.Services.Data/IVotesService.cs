namespace LocalFood.Services.Data
{
    using System.Threading.Tasks;

    public interface IVotesService
    {
        Task SetVoteAsync (int producerId, string userId, byte value);

        double GetAverageVote(int producerId);

        string GetProducerUserId(int producerId);
    }
}
