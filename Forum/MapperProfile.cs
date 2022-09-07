using AutoMapper;
using Data;
using Data.Models;
using Data.ViewModels;
using Data.ViewModels.Report;
using Data.ViewModels.Section;

namespace AlwaysForum; 

public class MapperProfile : Profile {
    public MapperProfile() {
        CreateMap<ForumUser, UserProfileViewModel>()
            .ForMember(dest => dest.PostCount, opt => opt.MapFrom(src => src.Posts.Count));
        CreateMap<Message, MessageViewModel>()
            .ForMember(dest => dest.SendDate, opt => opt.MapFrom(src => src.SendDate.ToString("G")));
        CreateMap<Section, SectionViewModel>();
        CreateMap<Section, SectionEditViewModel>();
        /*CreateMap<Section, SectionViewModel>()
            .ForMember(dest => dest.PostsModels, opt => opt.MapFrom(src => src.Posts));*/
        CreateMap<Post, SectionPostViewModel>()
            .ForMember(dest => dest.ShortenedDescription, opt => opt.MapFrom(src => src.Description.Substring(0, src.Description.Length >= GlobalConstants.MaximumPostDescriptionLength ? GlobalConstants.MaximumPostDescriptionLength : src.Description.Length)));
        CreateMap<PostReport, PostReportViewModel>()
            .ForMember(dest => dest.PostTitle, opt => opt.MapFrom(src => src.Post.Title))
            .ForMember(dest => dest.AuthorUserName, opt => opt.MapFrom(src => src.Author.UserName))
            .ForMember(dest => dest.ReportTypeName, opt => opt.MapFrom(src => src.ReportType.Name));
        CreateMap<CommentReport, CommentReportViewModel>()
            .ForMember(dest => dest.CommentText, opt => opt.MapFrom(src => src.Comment.Description))
            .ForMember(dest => dest.AuthorUserName, opt => opt.MapFrom(src => src.Author.UserName))
            .ForMember(dest => dest.ReportTypeName, opt => opt.MapFrom(src => src.ReportType.Name));
    }
}