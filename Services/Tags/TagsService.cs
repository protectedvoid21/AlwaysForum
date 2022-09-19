﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using Data.Models;
using Data.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Services.Tags; 

public class TagsService : ITagsService {
    private readonly ForumDbContext dbContext;
    private readonly IMapper mapper;

    public TagsService(ForumDbContext dbContext, IMapper mapper) {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task AddAsync(string name) {
        Tag tag = new() {
            Name = name,
        };

        await dbContext.AddAsync(tag);
        await dbContext.SaveChangesAsync();
    }

    public async Task AddToPost(int tagId, int postId) {
        PostTag postTag = new() {
            PostId = postId,
            TagId = tagId,
        };

        await dbContext.AddAsync(postTag);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, string name) {
        Tag? tag = await dbContext.Tags.FindAsync(id);
        if (tag == null) {
            return;
        }

        tag.Name = name;
        dbContext.Update(tag);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Tag>> GetAllAsync() {
        return dbContext.Tags;
    }

    public async Task<IEnumerable<TagViewModel>> GetTrendingForSection(int sectionId, int count) {
        return dbContext.Posts
            .Where(p => p.SectionId == sectionId)
            .SelectMany(p => p.Tags.Select(pt => pt.Tag))
            .ProjectTo<TagViewModel>(mapper.ConfigurationProvider)
            .OrderByDescending(t => t.PostCount)
            .Where(t => t.PostCount > 0)
            .Take(count);
    }

    public async Task DeleteAsync(int id) {
        Tag? tag = await dbContext.Tags.FindAsync(id);
        if(tag == null) {
            return;
        }

        dbContext.Remove(tag);
        await dbContext.SaveChangesAsync();
    }
}