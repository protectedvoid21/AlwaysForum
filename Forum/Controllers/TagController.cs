using AutoMapper;
using Data;
using Data.Models;
using Data.ViewModels.Tag;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Mvc;
using Services.Tags;

namespace AlwaysForum.Controllers;

[Authorize(Roles = GlobalConstants.AdminRoleName)]
public class TagController : Controller {
    private readonly ITagsService tagsService;
    private readonly IMapper mapper;

    public TagController(ITagsService tagsService, IMapper mapper) {
        this.tagsService = tagsService;
        this.mapper = mapper;
    }

    public async Task<IActionResult> All() {
        IEnumerable<Tag> tags = await tagsService.GetAllAsync();
        return View(tags);
    }

    [HttpGet]
    public IActionResult Add() => View();

    [HttpPost]
    public async Task<IActionResult> Add(TagCreateViewModel tagModel) {
        if (!ModelState.IsValid) {
            return View(tagModel);
        }

        await tagsService.AddAsync(tagModel.Name);
        return RedirectToAction("All");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id) {
        Tag tag = await tagsService.GetById(id);
        TagEditViewModel tagModel = mapper.Map<TagEditViewModel>(tag);
        return View(tagModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(TagEditViewModel tagModel) {
        if (!ModelState.IsValid) {
            return View(tagModel);
        }

        await tagsService.UpdateAsync(tagModel.Id, tagModel.Name);
        return RedirectToAction("All");
    }

    public async Task<IActionResult> Delete(int id) {
        await tagsService.DeleteAsync(id);
        return RedirectToAction("All");
    }
}