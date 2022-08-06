﻿using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.Posts; 

public class PostsService : IPostsService {
    private readonly ForumDbContext dbContext;

    public PostsService(ForumDbContext dbContext) {
        this.dbContext = dbContext;
    }

    public async Task<Post> GetById(int id) {
        Post post = await dbContext.Posts.FirstAsync(p => p.Id == id);
        return post;
    }

    public async Task<IEnumerable<Post>> GetBySection(int sectionId) {
        return dbContext.Posts.Where(p => p.SectionId == sectionId);
    }

    public async Task AddAsync(string title, string description, string authorId, int sectionId) {
        Post post = new() {
            Title = title,
            Description = description,
            AuthorId = authorId,
            SectionId = sectionId,
            CreatedDate = DateTime.Now,
        };
        await dbContext.AddAsync(post);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, string title, string description) {
        Post post = await dbContext.Posts.FindAsync(id);
        post.Title = title;
        post.Description = description;

        dbContext.Update(post);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id) {
        Post post = await dbContext.Posts.FindAsync(id);
        dbContext.Remove(post);
        await dbContext.SaveChangesAsync();
    }
}