﻿using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class ForumDbContext : IdentityDbContext<ForumUser, IdentityRole, string> {
    public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);

        builder.Entity<CommentVote>().HasOne(c => c.Comment).WithMany(c => c.Votes).OnDelete(DeleteBehavior.NoAction);
    }

    public DbSet<Section> Sections { get; set; }

    public DbSet<Post> Posts { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public DbSet<CommentVote> CommentUpVotes { get; set; }

    public DbSet<Reaction> Reactions { get; set; }
}