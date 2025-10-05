using AutoMapper;
using SubscriptionManagement.BLL.Interfaces;
using SubscriptionManagement.DAL.Infrasructure.Interfaces;
using SubscriptionManagement.Models.DTO.User;
using SubscriptionManagement.Models.Entities;

namespace SubscriptionManagement.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.Users.ListAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        /// <summary>
        /// Get user by Id
        /// </summary>
        public async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            return _mapper.Map<UserDto>(user);
        }

        /// <summary>
        /// Add a new user with validation
        /// </summary>
        public async Task<UserDto> AddUserAsync(UserInputDTO inputDto)
        {
            if (inputDto == null)
                throw new ArgumentNullException(nameof(inputDto));

            // Validate email uniqueness
            var existing = await _unitOfWork.Users.GetUserByEmailAsync(inputDto.Email);
            if (existing != null)
                throw new InvalidOperationException($"User with email '{inputDto.Email}' already exists");

            var user = _mapper.Map<User>(inputDto);
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<UserDto>(user);
        }

        /// <summary>
        /// Update an existing user
        /// </summary>
        public async Task UpdateUserAsync(Guid id, UserInputDTO inputDto)
        {
            if (inputDto == null)
                throw new ArgumentNullException(nameof(inputDto));

            var existing = await _unitOfWork.Users.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException("User not found");

            // Check email uniqueness
            if (!string.Equals(existing.Email, inputDto.Email, StringComparison.OrdinalIgnoreCase))
            {
                var duplicate = await _unitOfWork.Users.GetUserByEmailAsync(inputDto.Email);
                if (duplicate != null)
                    throw new InvalidOperationException($"Email '{inputDto.Email}' is already taken by another user");
            }

            _mapper.Map(inputDto, existing);
            _unitOfWork.Users.Update(existing);
            await _unitOfWork.CompleteAsync();
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            // prevent deletion if user has active subscriptions
            if (user.Subscriptions != null && user.Subscriptions.Any(s => s.Status == Models.Enums.SubscriptionStatus.Active))
                throw new InvalidOperationException("Cannot delete user with active subscriptions");

            _unitOfWork.Users.Delete(user);
            await _unitOfWork.CompleteAsync();
        }

        /// <summary>
        /// Get user by email
        /// </summary>
        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user = await _unitOfWork.Users.GetUserByEmailAsync(email);
            if (user == null)
                throw new KeyNotFoundException($"User with email '{email}' not found");

            return _mapper.Map<UserDto>(user);
        }
    }
}
