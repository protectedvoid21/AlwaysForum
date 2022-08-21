using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Data.ViewModels; 

public class ChangeProfilePictureViewModel {
    public string UserId { get; set; }

    public string ProfilePicture { get; set; }

    [Required]
    public IFormFile NewProfilePicture { get; set; }
}