using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class ForumDbContext : IdentityDbContext<ForumUser, IdentityRole, string> {
    public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options) { }

    public DbSet<Section> Sections { get; set; }

    public DbSet<Post> Posts { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public DbSet<Tag> Tags { get; set; }
}