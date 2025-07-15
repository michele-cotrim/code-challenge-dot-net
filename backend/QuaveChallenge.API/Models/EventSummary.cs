namespace QuaveChallenge.API.Models
{
    public class EventSummary
    {
        public int CommunityId { get; set; }
        public DateTime? LastCheckin { get; set; }
        public DateTime? LastCheckout { get; set; }
        public DateTime? FirstCheckin { get; set; }
        public DateTime? FirstCheckout { get; set; }
        public int CurrentAttendeeCount { get; set; }
        public int PeopleNotCheckedIn { get; set; }
        public List<CompanyBreakdown> CompanyBreakdown{  get; set; }
    }
}