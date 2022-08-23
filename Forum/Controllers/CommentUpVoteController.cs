using AlwaysForum.Extensions;
using Data.Dtos;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.CommentUpVotes;

namespace AlwaysForum.Controllers; 

[ApiController, Route("api/commentvote")]
public class CommentUpVoteController : ControllerBase {
    private readonly ICommentUpVotesService commentUpVotesService;

    public CommentUpVoteController(ICommentUpVotesService commentUpVotesService) {
        this.commentUpVotesService = commentUpVotesService;
    }

    [HttpGet("{commentId:int}")]
    public async Task<int> GetVoteCount(int commentId) {
        return await commentUpVotesService.GetVoteCount(commentId);
    }

    [Authorize]
    [HttpGet("isvoted/{commentId:int}")]
    public async Task<bool> IsVoted(int commentId) {
        return await commentUpVotesService.IsVotedByUser(commentId, User.GetId());
    }

    [Authorize]
    [HttpPost]
    public async Task Vote([FromBody] CommentVoteDto commentDto) {
        await commentUpVotesService.VoteAsync(commentDto.CommentId, User.GetId(), commentDto.IsUpVote);
    }
}