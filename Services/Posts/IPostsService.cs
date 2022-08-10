﻿using Data.Models;

namespace Services.Posts; 

public interface IPostsService {
    Task<Post> GetById(int id);

    Task<IEnumerable<Post>> GetBySection(int sectionId);

    Task<int> AddAsync(string title, string description, string authorId, int sectionId);

    Task UpdateAsync(int id, string title, string description);

    Task DeleteAsync(int id);
}