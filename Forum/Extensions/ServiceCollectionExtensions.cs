using AutoMapper;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Services.Comments;
using Services.CommentUpVotes;
using Services.Messages;
using Services.Posts;
using Services.Reactions;
using Services.Sections;
using Services.Users;

namespace AlwaysForum.Extensions;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddIdentity(this IServiceCollection serviceCollection) {
        serviceCollection.AddIdentity<ForumUser, IdentityRole>(options => {
            options.SignIn.RequireConfirmedEmail = false;
            options.Password.RequireNonAlphanumeric = false;
            options.User.RequireUniqueEmail = true;
        }).AddEntityFrameworkStores<ForumDbContext>();
        return serviceCollection;
    }

    public static IServiceCollection AddServices(this IServiceCollection serviceCollection) {
        return serviceCollection
            .AddAutoMapper(typeof(MapperProfile))
            .AddTransient<IUsersService, UsersService>()
            .AddTransient<ISectionsService, SectionsService>()
            .AddTransient<IPostsService, PostsService>()
            .AddTransient<IReactionsService, ReactionsService>()
            .AddTransient<ICommentsService, CommentsService>()
            .AddTransient<ICommentVotesService, CommentVotesService>()
            .AddTransient<IMessagesService, MessagesService>();
    }
}