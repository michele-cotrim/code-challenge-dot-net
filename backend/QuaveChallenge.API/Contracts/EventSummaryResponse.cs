using Newtonsoft.Json;
using QuaveChallenge.API.Models;

namespace QuaveChallenge.API.Contracts
{
    public class EventSummaryResponse
    {
        [JsonProperty(PropertyName = "communityId")]
        public int CommunityId { get; set; }

        [JsonProperty(PropertyName = "LastCheckin")]
        public DateTime? LastCheckin { get; set; }

        [JsonProperty(PropertyName = "LastCheckout")]
        public DateTime? LastCheckout { get; set; }

        [JsonProperty(PropertyName = "firstCheckin")]
        public DateTime? FirstCheckin { get; set; }

        [JsonProperty(PropertyName = "firstCheckout")]
        public DateTime? FirstCheckout { get; set; }

        [JsonProperty(PropertyName = "currentAttendeeCount")]
        public int CurrentAttendeeCount { get; set; }

        [JsonProperty(PropertyName = "peopleNotCheckedin")]
        public int PeopleNotCheckedIn { get; set; }

        [JsonProperty(PropertyName = "companyBreakdown")]
        public List<CompanyBreakdownResponse> CompanyBreakdown { get; set; }
    }
}
