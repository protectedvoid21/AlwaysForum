using AlwaysForum.Extensions;
using Data;
using Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Comments;

namespace AlwaysForum.Controllers;

[Authorize]
public class CommentController : Controller {
    private readonly ICommentsService commentsService;

    public CommentController(ICommentsService commentsService) {
        this.commentsService = commentsService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CommentCreateViewModel commentModel) {
        await commentsService.AddAsync(commentModel.Description, commentModel.PostId, User.GetId());

        return RedirectToAction("View", "Post", new { postId = commentModel.PostId });
    }
}