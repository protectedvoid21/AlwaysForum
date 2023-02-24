using AlwaysForum.Extensions;
using AutoMapper;
using Data;
using Data.Models;
using Data.ViewModels;
using Data.ViewModels.Post;
using Microsoft.AspNetCore.Mvc;
using Services.Comments;
using Services.Posts;
using Services.Tags;

namespace AlwaysForum.Controllers;

public class PostController : Controller {
    private readonly IPostsService postsService;
    private readonly ICommentsService commentsService;
    private readonly ITagsService tagsService;
    private readonly IMapper mapper;

    public PostController(IPostsService postsService, ICommentsService commentsService, ITagsService tagsService, IMapper mapper) {
        this.postsService = postsService;
        this.commentsService = commentsService;
        this.tagsService = tagsService;
        this.mapper = mapper;
    }

    public async Task<IActionResult> View(int id) {
        Post post = await postsService.GetById(id);
        post.Comments = (await commentsService.GetByPost(id)).OrderByDescending(c => c.CreatedTime).ToList();

        if (User.Identity.IsAuthenticated) {
            ViewBag.IsAuthor = await postsService.IsAuthor(id, User.GetId());
        }
        return View(post);
    }

    public async Task<IActionResult> Add(int sectionId) {
        if (!User.Identity.IsAuthenticated) {
            return RedirectToAction("Login", "Account");
        }

        PostCreateViewModel postModel = new() {
            SectionId = sectionId,
            TagList = await tagsService.GetAllAsync(),
        };

        return View(postModel);
    }

    [HttpPost]
    public async Task<IActionResult> Add(PostCreateViewModel postModel) {
        if (!ModelState.IsValid) {
            return View(postModel);
        }

        int createdId = await postsService.AddAsync(postModel.Title, postModel.Description, User.GetId(), postModel.SectionId, postModel.SelectedTags);
        return RedirectToAction("View", "Post", new { postId = createdId });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int postId) {
        Post post = await postsService.GetById(postId);

        if (!await postsService.IsAuthor(postId, User.GetId())) {
            return Forbid();
        }

        PostEditViewModel postModel = mapper.Map<PostEditViewModel>(post);
        postModel.TagList = await tagsService.GetAllAsync();

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

        await postsService.UpdateAsync(postModel.Id, postModel.Title, postModel.Description, postModel.SelectedTags);
        return RedirectToAction("View", "Post", new { id = postModel.Id });
    }

    public async Task<IActionResult> Delete(int postId) {
        if(!await postsService.IsAuthor(postId, User.GetId()) && !User.IsInRole(GlobalConstants.AdminRoleName)) {
            return Forbid();
        }

        await postsService.DeleteAsync(postId);
        return InfoHelper.RedirectToMessage("Post has been successfully deleted", InfoType.Success);
    }
}