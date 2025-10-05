using AutoMapper;
using SubscriptionManagement.Models.DTO.Plan;
using SubscriptionManagement.Models.Entities;
using SubscriptionManagement.Models.ViewModels.Plan;

namespace SubscriptionManagement.Models.MappingProfiles.PlanProfile
{
    public class PlanProfile : Profile
    {
        public PlanProfile()
        {

            CreateMap<PlanDto, PlanListViewModel>().ReverseMap();
            CreateMap<PlanDto, PlanViewModel>().ReverseMap();
            CreateMap<Plan, PlanDto>().ReverseMap();
        }
    }
}
