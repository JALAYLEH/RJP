using AutoMapper;
using SubscriptionManagement.Models.DTO.Subscription;
using SubscriptionManagement.Models.Entities;
using SubscriptionManagement.Models.Enums;
using SubscriptionManagement.Models.ViewModels.Subscription;

namespace SubscriptionManagement.Models.MappingProfiles.SubscriptionProfile
{
    public class SubscriptionProfile : Profile
    {
        public SubscriptionProfile()
        {
            CreateMap<SubscriptionDto, SubscriptionListViewModel>().ReverseMap();
            CreateMap<SubscriptionInputDto, SubscriptionViewModel>().ReverseMap();
            CreateMap<SubscriptionDto, SubscriptionViewModel>().ReverseMap();
            CreateMap<Subscription, SubscriptionDto>()
           .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.FullName))
           .ForMember(dest => dest.PlanName, opt => opt.MapFrom(src => src.Plan.Name))
           .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<SubscriptionDto, Subscription>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<SubscriptionStatus>(src.Status)))
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Plan, opt => opt.Ignore());

            CreateMap<SubscriptionInputDto, Subscription>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Plan, opt => opt.Ignore());


            CreateMap<Subscription, SubscriptionInputDto>();

        }
    }
}
