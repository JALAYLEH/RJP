using AutoMapper;
using SubscriptionManagement.BLL.Interfaces;
using SubscriptionManagement.DAL.Infrasructure.Interfaces;
using SubscriptionManagement.Models.DTO.Plan;
using SubscriptionManagement.Models.Entities;
using SubscriptionManagement.Models.Enums;

namespace SubscriptionManagement.BLL.Services
{
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PlanService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Adds a new plan after validation.
        /// </summary>
        public async Task AddPlanAsync(PlanDto planDto)
        {
            if (planDto == null)
                throw new ArgumentNullException(nameof(planDto));

            if (string.IsNullOrWhiteSpace(planDto.Name))
                throw new ArgumentException("Plan name cannot be empty");

            if (planDto.PricePerMonth < 0)
                throw new ArgumentException("Price must be positive");

            var planEntity = _mapper.Map<Plan>(planDto);
            await _unitOfWork.Plans.AddAsync(planEntity);
            await _unitOfWork.CompleteAsync();
        }

        /// <summary>
        /// Deletes a plan if no active subscriptions exist.
        /// </summary>
        public async Task DeletePlanAsync(Guid id)
        {
            var plan = await _unitOfWork.Plans.GetPlanWithSubscriptionsAsync(id);
            if (plan == null)
                throw new KeyNotFoundException("Plan not found");

            // Business rule: cannot delete plan with active subscriptions
            if (plan.Subscriptions != null && plan.Subscriptions.Any(s => s.Status == SubscriptionStatus.Active))
                throw new InvalidOperationException("Cannot delete plan with active subscriptions");

            _unitOfWork.Plans.Delete(plan);
            await _unitOfWork.CompleteAsync();
        }

        /// <summary>
        /// Returns all plans as DTOs.
        /// </summary>
        public async Task<IEnumerable<PlanDto>> GetAllPlansAsync()
        {
            var plans = await _unitOfWork.Plans.ListAllAsync();
            return _mapper.Map<IEnumerable<PlanDto>>(plans);
        }

        /// <summary>
        /// Returns a single plan by Id.
        /// </summary>
        public async Task<PlanDto> GetPlanByIdAsync(Guid id)
        {
            var plan = await _unitOfWork.Plans.GetByIdAsync(id);
            if (plan == null)
                throw new KeyNotFoundException("Plan not found");

            return _mapper.Map<PlanDto>(plan);
        }

        /// <summary>
        /// Updates an existing plan after validation.
        /// </summary>
        public async Task UpdatePlanAsync(PlanDto planDto)
        {
            if (planDto == null)
                throw new ArgumentNullException(nameof(planDto));

            var existingPlan = await _unitOfWork.Plans.GetByIdAsync(planDto.Id);
            if (existingPlan == null)
                throw new KeyNotFoundException("Plan not found");

            if (string.IsNullOrWhiteSpace(planDto.Name))
                throw new ArgumentException("Plan name cannot be empty");

            if (planDto.PricePerMonth < 0)
                throw new ArgumentException("Price must be positive");

            _mapper.Map(planDto, existingPlan);
            _unitOfWork.Plans.Update(existingPlan);
            await _unitOfWork.CompleteAsync();
        }

        /// <summary>
        /// Retrieves a plan including its subscriptions.
        /// </summary>
        public async Task<PlanDto?> GetPlanWithSubscriptionsAsync(Guid id)
        {
            var plan = await _unitOfWork.Plans.GetPlanWithSubscriptionsAsync(id);
            if (plan == null)
                throw new KeyNotFoundException("Plan not found");

            return _mapper.Map<PlanDto>(plan);
        }
    }
}
