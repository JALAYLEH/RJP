namespace SubscriptionManagement.Models.DTO.Subscription
{
    public class SubscriptionDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PlanId { get; set; }
        public string UserFullName { get; set; }
        public string PlanName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
    }
}
