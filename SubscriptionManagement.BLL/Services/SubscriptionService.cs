using AutoMapper;
using SubscriptionManagement.BLL.Interfaces;
using SubscriptionManagement.DAL.Infrasructure.Interfaces;
using SubscriptionManagement.DAL.Infrastructure.Specifications;
using SubscriptionManagement.Models.DTO.Subscription;
using SubscriptionManagement.Models.Entities;

namespace SubscriptionManagement.BLL.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubscriptionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Add a new subscription with validation.
        /// </summary>
        public async Task<SubscriptionDto> AddSubscriptionAsync(SubscriptionInputDto inputDto)
        {
            if (inputDto == null)
                throw new ArgumentNullException(nameof(inputDto));

            // Plan and User must exist
            var plan = await _unitOfWork.Plans.GetByIdAsync(inputDto.PlanId);
            if (plan == null)
                throw new KeyNotFoundException("Plan not found");

            var user = await _unitOfWork.Users.GetByIdAsync(inputDto.UserId);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            // Default start date if not provided
            if (!inputDto.StartDate.HasValue)
                inputDto.StartDate = DateTime.UtcNow;


            var subscription = _mapper.Map<Subscription>(inputDto);

            // Save subscription
            await _unitOfWork.Subscriptions.AddAsync(subscription);
            await _unitOfWork.CompleteAsync();

            // Reload subscription with navigation properties using repository method
            //var subscriptionWithNav = await _unitOfWork.Subscriptions
            //    .GetByIdIncludingAsync(subscription.Id, s => s.User, s => s.Plan);
            var spec = new SubscriptionWithUserAndPlanSpecification();
            var subscriptionWithNav = await _unitOfWork.Subscriptions.GetByIdAsync(subscription.Id, spec);


            return _mapper.Map<SubscriptionDto>(subscriptionWithNav);
        }


        /// <summary>
        /// Delete a subscription by Id.
        /// </summary>
        public async Task DeleteSubscriptionAsync(Guid id)
        {
            var subscription = await _unitOfWork.Subscriptions.GetByIdAsync(id);
            if (subscription == null)
                throw new KeyNotFoundException("Subscription not found");

            _unitOfWork.Subscriptions.Delete(subscription);
            await _unitOfWork.CompleteAsync();
        }

        /// <summary>
        /// Get all subscriptions.
        /// </summary>
        public async Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsAsync()
        {
            var spec = new SubscriptionWithUserAndPlanSpecification();
            var subscriptions = await _unitOfWork.Subscriptions.ListAsync(spec);
            return _mapper.Map<IEnumerable<SubscriptionDto>>(subscriptions);
        }

        /// <summary>
        /// Get subscription by Id.
        /// </summary>
        public async Task<SubscriptionDto> GetSubscriptionByIdAsync(Guid id)
        {
            var spec = new SubscriptionWithUserAndPlanSpecification();
            var subscription = await _unitOfWork.Subscriptions.GetByIdAsync(id, spec);

            if (subscription == null)
                throw new KeyNotFoundException("Subscription not found");

            return _mapper.Map<SubscriptionDto>(subscription);
        }

        /// <summary>
        /// Update subscription.
        /// </summary>
        public async Task UpdateSubscriptionAsync(Guid id, SubscriptionInputDto inputDto)
        {
            if (inputDto == null)
                throw new ArgumentNullException(nameof(inputDto));

            var existing = await _unitOfWork.Subscriptions.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException("Subscription not found");

            // Validate Plan and User
            var plan = await _unitOfWork.Plans.GetByIdAsync(inputDto.PlanId);
            if (plan == null)
                throw new KeyNotFoundException("Plan not found");

            var user = await _unitOfWork.Users.GetByIdAsync(inputDto.UserId);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            // Map updated properties onto existing subscription
            _mapper.Map(inputDto, existing);

            _unitOfWork.Subscriptions.Update(existing);
            await _unitOfWork.CompleteAsync();
        }
    }
}
