using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AlwaysForum.Components;

[ViewComponent(Name = "CreateComment")]
public class CreateCommentViewComponent : ViewComponent {
    public async Task<IViewComponentResult> InvokeAsync(int postId) {
        CommentCreateViewModel commentModel = new() {
            PostId = postId,
        };

        return View(commentModel);
    }
}