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

    public DbSet<CommentVote> CommentUpVotes { get; set; }

    public DbSet<Reaction> Reactions { get; set; }

    public DbSet<Message> Messages { get; set; }

    public DbSet<PostReport> PostReports { get; set; }

    public DbSet<CommentReport> CommentReports { get; set; }

    public DbSet<ReportType> ReportTypes { get; set; }
}