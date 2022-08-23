using AlwaysForum.Extensions;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Reactions;

namespace AlwaysForum.Controllers {
    [Route("api/reaction")]
    [ApiController]
    public class ReactionController : ControllerBase {
        private readonly IReactionsService reactionsService;

        public ReactionController(IReactionsService reactionsService) {
            this.reactionsService = reactionsService;
        }

        [HttpGet("{postId:int}")]
        public async Task<IEnumerable<Reaction>> GetPostReactions(int postId) {
            return await reactionsService.GetByPost(postId);
        }

        [Authorize]
        [HttpPost("like/{postId:int}")]
        public async Task Like(int postId) {
            await reactionsService.ReactAsync(postId, User.GetId(), ReactionType.Like);
        }

        [Authorize]
        [HttpPost("love/{postId:int}")]
        public async Task Love(int postId) {
            await reactionsService.ReactAsync(postId, User.GetId(), ReactionType.Love);
        }

        [Authorize]
        [HttpPost("wow/{postId:int}")]
        public async Task Wow(int postId) {
            await reactionsService.ReactAsync(postId, User.GetId(), ReactionType.Wow);
        }

        [Authorize]
        [HttpPost("sad/{postId:int}")]
        public async Task Sad(int postId) {
            await reactionsService.ReactAsync(postId, User.GetId(), ReactionType.Sad);
        }

        [Authorize]
        [HttpPost("angry/{postId:int}")]
        public async Task Angry(int postId) {
            await reactionsService.ReactAsync(postId, User.GetId(), ReactionType.Angry);
        }
    }
}
