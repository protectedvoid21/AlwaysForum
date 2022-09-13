using Microsoft.AspNetCore.Identity;

namespace Data.Models; 

public class ForumUser : IdentityUser {
    public string? ProfilePicture { get; set; }

    public DateTime CreatedDate { get; set; }

    public List<Post> Posts { get; set; } = new();

    public List<Comment> Comments { get; set; } = new();
}