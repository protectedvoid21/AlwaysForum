﻿using System.Collections;
using Data;
using Data.Models;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Posts;
using Services.Sections;

namespace AlwaysForum.Controllers; 

public class SectionController : Controller {
    private readonly ISectionsService sectionsService;
    private readonly IPostsService postsService;

    public SectionController(ISectionsService sectionsService, IPostsService postsService) {
        this.sectionsService = sectionsService;
        this.postsService = postsService;
    }

    public async Task<IActionResult> ViewSection(int sectionId) {
        Section section = await sectionsService.GetById(sectionId);
        SectionViewModel sectionModel = new() {
            Id = sectionId,
            Name = section.Name,
            Description = section.Description,
            Posts = await postsService.GetBySection(sectionId),
        };

        foreach (var post in sectionModel.Posts) {
            string desc = post.Description;
            int substringLength = desc.Length >= GlobalConstants.MaximumPostDescriptionLength ? GlobalConstants.MaximumPostDescriptionLength : desc.Length;
            post.Description = post.Description[..substringLength];
        }
        return View(sectionModel);
    }
}