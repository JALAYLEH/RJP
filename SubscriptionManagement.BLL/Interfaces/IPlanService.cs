using SubscriptionManagement.Models.DTO.Plan;

namespace SubscriptionManagement.BLL.Interfaces
{
    public interface IPlanService
    {
        Task<IEnumerable<PlanDto>> GetAllPlansAsync();
        Task<PlanDto> GetPlanByIdAsync(Guid id);
        Task AddPlanAsync(PlanDto plan);
        Task UpdatePlanAsync(PlanDto plan);
        Task DeletePlanAsync(Guid id);
    }
}
