using Newtonsoft.Json;
using QuaveChallenge.API.Models;

namespace QuaveChallenge.API.Contracts
{
    public class PersonResponse
    {
        [JsonProperty(PropertyName = "peopleName")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "fullName")]
        public string FullName { get; set; }

        [JsonProperty(PropertyName = "companyName")]
        public string? CompanyName { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string? Title { get; set; }

        [JsonProperty(PropertyName = "communityId")]
        public int? CommunityId { get; set; }

        [JsonProperty(PropertyName = "community")]
        public Community? Community { get; set; }
    }
}
