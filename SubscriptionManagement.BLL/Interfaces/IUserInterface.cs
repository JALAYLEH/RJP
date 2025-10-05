using SubscriptionManagement.Models.DTO.User;

namespace SubscriptionManagement.BLL.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(Guid id);
        Task<UserDto> AddUserAsync(UserInputDTO inputDto);
        Task UpdateUserAsync(Guid id, UserInputDTO inputDto);
        Task DeleteUserAsync(Guid id);
        Task<UserDto> GetUserByEmailAsync(string email);
    }
}
