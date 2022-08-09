using AlwaysForum.Extensions;
using Data;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Comments;

namespace AlwaysForum.Controllers; 

public class CommentController : Controller {
    private readonly ICommentsService commentsService;

    public CommentController(ICommentsService commentsService) {
        this.commentsService = commentsService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CommentCreateViewModel commentModel) {
        await commentsService.AddAsync(commentModel.Description, commentModel.PostId, User.GetById());

        return RedirectToAction("View", "Post", new { postId = commentModel.PostId });
    }
}