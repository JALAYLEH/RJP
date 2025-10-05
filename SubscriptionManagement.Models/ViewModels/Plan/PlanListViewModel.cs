namespace SubscriptionManagement.Models.ViewModels.Plan
{
    public class PlanListViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerMonth { get; set; }


        public string FormattedPrice => $"${PricePerMonth:N2}";
    }
}
