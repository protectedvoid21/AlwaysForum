using AlwaysForum.Extensions;
using Data;
using Data.Models;
using Data.ViewModels;
using Data.ViewModels.Post;
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
        post.Comments = (await commentsService.GetByPost(postId)).OrderByDescending(c => c.CreatedTime).ToList();

        if (User.Identity.IsAuthenticated) {
            ViewBag.IsAuthor = await postsService.IsAuthor(postId, User.GetId());
        }
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

        int createdId = await postsService.AddAsync(postModel.Title, postModel.Description, User.GetId(), postModel.SectionId);
        return RedirectToAction("View", "Post", new { postId = createdId });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int postId) {
        Post post = await postsService.GetById(postId);

        if (!await postsService.IsAuthor(postId, User.GetId())) {
            return Forbid();
        }

        PostEditViewModel postModel = new() {
            Id = postId,
            Title = post.Title,
            Description = post.Description,
        };

        return View(postModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(PostEditViewModel postModel) {
        if (!ModelState.IsValid) {
            return View(postModel);
        }

        if(!await postsService.IsAuthor(postModel.Id, User.GetId())) {
            return Forbid();
        }

        await postsService.UpdateAsync(postModel.Id, postModel.Title, postModel.Description);
        return RedirectToAction("View", "Post", new { postId = postModel.Id });
    }

    public async Task<IActionResult> Delete(int postId) {
        if(!await postsService.IsAuthor(postId, User.GetId()) && !User.IsInRole(GlobalConstants.AdminRoleName)) {
            return Forbid();
        }

        await postsService.DeleteAsync(postId);
        return InfoHelper.RedirectToMessage("Post has been successfully deleted", InfoType.Success);
    }
}