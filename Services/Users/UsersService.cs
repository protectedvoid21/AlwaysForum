using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Models;
using Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Services.Users; 

public class UsersService : IUsersService {
    private readonly UserManager<ForumUser> userManager;
    private readonly IMapper mapper;
    
    public UsersService(UserManager<ForumUser> userManager, IMapper mapper) {
        this.userManager = userManager;
        this.mapper = mapper;
    }

    public async Task<TUser> GetAsync<TUser>(string id) {
        return await userManager.Users
            .Where(u => u.Id == id)
            .ProjectTo<TUser>(mapper.ConfigurationProvider)
            .FirstAsync();
    }

    public async Task<UserProfileViewModel> GetProfile(string userId) {
        var user = await userManager.FindByIdAsync(userId);
        UserProfileViewModel profileModel = mapper.Map<UserProfileViewModel>(user);

        return profileModel;
    }

    public async Task ChangeProfilePicture(string userId, IFormFile profilePicture) {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/userpictures", userId);
        filePath += Path.GetExtension(profilePicture.FileName);

        await using FileStream fileStream = new(filePath, FileMode.Create);
        await profilePicture.CopyToAsync(fileStream);

        ForumUser user = await userManager.FindByIdAsync(userId);
        user.ProfilePicture = Path.GetFileName(filePath);
        await userManager.UpdateAsync(user);
    }

    public async Task<IdentityResult> AddAsync(string userName, string password, string email) {
        ForumUser user = new() {
            Email = email,
            CreatedDate = DateTime.Now,
        };

        IdentityResult result = await userManager.CreateAsync(user, password);
        return result;
    }
}