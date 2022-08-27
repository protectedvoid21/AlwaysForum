using AlwaysForum.Extensions;
using Data.Dtos;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.CommentUpVotes;

namespace AlwaysForum.Controllers; 

[ApiController, Route("api/commentvote")]
public class CommentUpVoteController : ControllerBase {
    private readonly ICommentVotesService commentVotesService;

    public CommentUpVoteController(ICommentVotesService commentVotesService) {
        this.commentVotesService = commentVotesService;
    }

    [HttpGet("{commentId:int}")]
    public async Task<int> GetVoteCount(int commentId) {
        return await commentVotesService.GetVoteCount(commentId);
    }

    [Authorize]
    [HttpGet("isvoted/{commentId:int}")]
    public async Task<CommentVoteStatus> IsVoted(int commentId) {
        return await commentVotesService.IsVotedByUser(commentId, User.GetId());
    }

    [Authorize]
    [HttpPost]
    public async Task Vote([FromBody] CommentVoteDto commentDto) {
        await commentVotesService.VoteAsync(commentDto.CommentId, User.GetId(), commentDto.IsUpVote);
    }
}