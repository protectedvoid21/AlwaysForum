using System.Collections;
using Data;
using Data.Models;
using Data.ViewModels.Section;
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

    public SectionController(ISectionsService sectionsService, IPostsService postsService, ICommentsService commentsService) {
        this.sectionsService = sectionsService;
        this.postsService = postsService;
        this.commentsService = commentsService;
    }

    public async Task<IActionResult> View(int sectionId) {
        Section section = await sectionsService.GetById(sectionId);
        SectionViewModel sectionModel = new() {
            Id = sectionId,
            Name = section.Name,
            Description = section.Description,
        };

        IEnumerable<Post> posts = (await postsService.GetBySection(sectionId)).OrderByDescending(p => p.CreatedDate);
        List<SectionPostViewModel> postModelsList = new();

        //todo: use mapping
        foreach (var post in posts) {
            string desc = post.Description;
            int substringLength = desc.Length >= GlobalConstants.MaximumPostDescriptionLength ? GlobalConstants.MaximumPostDescriptionLength : desc.Length;

            postModelsList.Add(new SectionPostViewModel {
                Id = post.Id,
                ShortenedDescription = post.Description[..substringLength],
                AuthorId = post.AuthorId,
                AuthorName = post.Author.UserName,
                Title = post.Title,
                CommentCount = await commentsService.GetCountInPost(post.Id),
                CreatedDate = post.CreatedDate
            });
        }

        sectionModel.PostsModels = postModelsList;
        return View(sectionModel);
    }
}