using Data.Models;
using Data.ViewModels.Post;

namespace Data.ViewModels; 

public class UserProfileViewModel {
    public string Id { get; set; }

    public string UserName { get; set; }

    public string ProfilePicture { get; set; }

    public int PostCount { get; set; }

    public DateTime CreatedDate { get; set; }

    public IEnumerable<PostProfileViewModel> Posts { get; set; }
}