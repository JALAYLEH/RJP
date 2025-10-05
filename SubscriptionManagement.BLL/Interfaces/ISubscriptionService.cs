using SubscriptionManagement.Models.DTO.Subscription;

namespace SubscriptionManagement.BLL.Interfaces
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsAsync();
        Task<SubscriptionDto> GetSubscriptionByIdAsync(Guid id);
        Task<SubscriptionDto> AddSubscriptionAsync(SubscriptionInputDto inputDto);
        Task UpdateSubscriptionAsync(Guid id, SubscriptionInputDto inputDto);
        Task DeleteSubscriptionAsync(Guid id);
    }

}
