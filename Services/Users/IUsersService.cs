using Data.Models;
using Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Services.Users; 

public interface IUsersService {
    Task<IdentityResult> AddAsync(string userName, string password, string email);

    Task<UserProfileViewModel> GetProfile(string userId);

    Task ChangeProfilePicture(string userId, IFormFile profilePicture);
}