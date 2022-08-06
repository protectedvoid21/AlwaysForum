using Microsoft.AspNetCore.Identity;

namespace Data.Models; 

public class ForumUser : IdentityUser {
    public List<Post> Posts { get; set; }
}