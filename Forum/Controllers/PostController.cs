using AlwaysForum.Extensions;
using Data.Models;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Comments;
using Services.Posts;

namespace AlwaysForum.Controllers;

public class PostController : Controller {
    private readonly IPostsService postsService;
    private readonly ICommentsService commentsService;

    public PostController(IPostsService postsService, ICommentsService commentsService) {
        this.postsService = postsService;
        this.commentsService = commentsService;
    }

    public async Task<IActionResult> View(int postId) {
        Post post = await postsService.GetById(postId);
        post.Comments = (await commentsService.GetByPost(postId)).ToList();
        return View(post);
    }

    public async Task<IActionResult> Add(int sectionId) {
        if (!User.Identity.IsAuthenticated) {
            return RedirectToAction("Login", "Account");
        }

        PostCreateViewModel postModel = new() {
            SectionId = sectionId
        };

        return View(postModel);
    }

    [HttpPost]
    public async Task<IActionResult> Add(PostCreateViewModel postModel) {
        if (!ModelState.IsValid) {
            return View(postModel);
        }

        int createdId = await postsService.AddAsync(postModel.Title, postModel.Description, User.GetById(), postModel.SectionId);
        return RedirectToAction("View", "Post", new { postId = createdId });
    }
}