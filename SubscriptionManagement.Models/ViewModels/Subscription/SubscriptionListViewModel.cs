namespace SubscriptionManagement.Models.ViewModels.Subscription
{

    public class SubscriptionListViewModel
    {
        public Guid Id { get; set; }
        public string UserFullName { get; set; }
        public string PlanName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
    }

}
