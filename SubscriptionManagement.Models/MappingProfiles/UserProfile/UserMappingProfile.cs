using AutoMapper;
using SubscriptionManagement.Models.DTO.User;
using SubscriptionManagement.Models.Entities;
using SubscriptionManagement.Models.ViewModels.User;

namespace SubscriptionManagement.Models.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserDto, UserListViewModel>().ReverseMap();
            CreateMap<UserDto, UserViewModel>().ReverseMap();
            CreateMap<UserInputDTO, UserViewModel>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<UserInputDTO, User>().ReverseMap();
        }
    }
}
