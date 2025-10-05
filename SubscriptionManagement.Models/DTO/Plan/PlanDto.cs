namespace SubscriptionManagement.Models.DTO.Plan
{
    public class PlanDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerMonth { get; set; }
    }
}
