using AlwaysForum.Extensions;
using Data;
using Data.Models;
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

    public async Task<IActionResult> Delete(int id) {
        if (!User.IsInRole(GlobalConstants.AdminRoleName) && !await commentsService.IsAuthor(id, User.GetId())) {
            return Forbid();
        }

        await commentsService.DeleteAsync(id);
        return InfoHelper.RedirectToMessage("Comment has been deleted", InfoType.Success);
    }
}