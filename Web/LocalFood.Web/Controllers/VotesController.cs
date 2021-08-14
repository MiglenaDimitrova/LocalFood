namespace LocalFood.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using LocalFood.Services.Data;
    using LocalFood.Web.ViewModels.Votes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : BaseController
    {
        private readonly IVotesService votesService;
        private readonly IProducersService producersService;

        public VotesController(
            IVotesService votesService,
            IProducersService producersService)
        {
            this.votesService = votesService;
            this.producersService = producersService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<VoteResponseModel>> Post(VoteInputModel input)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var producerUserId = this.producersService.GetProducerUserId(input.ProducerId);
            if (userId == producerUserId)
            {
                return new VoteResponseModel { AverageVote = this.votesService.GetAverageVote(input.ProducerId) };
            }

            await this.votesService.SetVoteAsync(input.ProducerId, userId, input.Value);
            var averageVote = this.votesService.GetAverageVote(input.ProducerId);
            return new VoteResponseModel { AverageVote = averageVote };
        }
    }
}
