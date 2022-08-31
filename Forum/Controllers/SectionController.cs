using System.Collections;
using AutoMapper;
using Data;
using Data.Models;
using Data.ViewModels;
using Data.ViewModels.Section;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Comments;
using Services.Posts;
using Services.Sections;

namespace AlwaysForum.Controllers;

public class SectionController : Controller {
    private readonly ISectionsService sectionsService;
    private readonly IPostsService postsService;
    private readonly ICommentsService commentsService;
    private readonly IMapper mapper;

    public SectionController(
        ISectionsService sectionsService,
        IPostsService postsService,
        ICommentsService commentsService,
        IMapper mapper) {
        this.sectionsService = sectionsService;
        this.postsService = postsService;
        this.commentsService = commentsService;
        this.mapper = mapper;
    }

    public async Task<IActionResult> View(int sectionId) {
        Section section = await sectionsService.GetById(sectionId);
        SectionViewModel sectionModel = mapper.Map<SectionViewModel>(section);

        IEnumerable<Post> posts = (await postsService.GetBySection(sectionId)).OrderByDescending(p => p.CreatedDate);
        List<SectionPostViewModel> postModelsList = new();

        foreach (var post in posts) {
            var postModel = mapper.Map<SectionPostViewModel>(post);
            postModel.CommentCount = await postsService.GetCommentCount(post.Id);

            postModelsList.Add(postModel);
        }

        sectionModel.PostsModels = postModelsList;
        return View(sectionModel);
    }

    [HttpGet, Authorize(Roles = GlobalConstants.AdminRoleName)]
    public IActionResult Add() => View();

    [HttpPost, Authorize(Roles = GlobalConstants.AdminRoleName)]
    public async Task<IActionResult> Add(SectionCreateViewModel sectionModel) {
        if(!ModelState.IsValid) {
            return View(sectionModel);
        }

        await sectionsService.AddAsync(sectionModel.Name, sectionModel.Description);

        return RedirectToAction("Panel", "Admin");
    }

    [Authorize(Roles = GlobalConstants.AdminRoleName)]
    public async Task<IActionResult> SectionList() {
        IEnumerable<Section> sectionList = await sectionsService.GetAll();
        return View(sectionList);
    }

    [HttpGet, Authorize(Roles = GlobalConstants.AdminRoleName)]
    public async Task<IActionResult> Edit(int id) {
        Section section = await sectionsService.GetById(id);
        var sectionModel = mapper.Map<SectionEditViewModel>(section);

        return View(sectionModel);
    }

    [HttpPost, Authorize(Roles = GlobalConstants.AdminRoleName)]
    public async Task<IActionResult> Edit(SectionEditViewModel sectionModel) {
        if (!ModelState.IsValid) {
            return View(sectionModel);
        }

        await sectionsService.UpdateAsync(sectionModel.Id, sectionModel.Name, sectionModel.Description);
        return RedirectToAction("View", sectionModel.Id);
    }

    [HttpPost, Authorize(Roles = GlobalConstants.AdminRoleName)]
    public async Task<IActionResult> Delete(int id) {
        await sectionsService.DeleteAsync(id);
        return InfoHelper.RedirectToMessage("Post has been deleted", InfoType.Success);
    }
}