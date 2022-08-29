using AutoMapper;
using Data.Models;
using Data.ViewModels;
using Data.ViewModels.Section;

namespace AlwaysForum; 

public class MapperProfile : Profile {
    public MapperProfile() {
        CreateMap<ForumUser, UserProfileViewModel>()
            .ForMember(dest => dest.PostCount, opt => opt.MapFrom(src => src.Posts.Count));
        CreateMap<Message, MessageViewModel>()
            .ForMember(dest => dest.SendDate, opt => opt.MapFrom(src => src.SendDate.ToString("G")));
        /*CreateMap<Section, SectionViewModel>()
            .ForMember(dest => dest.PostsModels, opt => opt.MapFrom(src => src.Posts));
        CreateMap<IEnumerable<Post>, IEnumerable<SectionPostViewModel>>()*/
    }
}