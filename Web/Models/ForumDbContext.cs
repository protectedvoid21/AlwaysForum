using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlwaysForum.Models; 

public class ForumDbContext : IdentityDbContext<ForumUser, IdentityRole, string> {
    public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options) { }
}