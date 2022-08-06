using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Posts;

namespace AlwaysForum.Controllers;

public class PostController : Controller {
    private readonly IPostsService postsService;

    public PostController(IPostsService postsService) {
        this.postsService = postsService;
    }

    public async Task<IActionResult> View(int postId) {
        Post post = await postsService.GetById(postId);
        return View(post);
    }
}