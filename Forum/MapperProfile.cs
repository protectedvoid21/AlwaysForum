using AutoMapper;
using Data.Models;
using Data.ViewModels;

namespace AlwaysForum; 

public class MapperProfile : Profile {
    public MapperProfile() {
        CreateMap<ForumUser, UserProfileViewModel>()
            .ForMember(dest => dest.PostCount, opt => opt.MapFrom(src => src.Posts.Count));
    }
}