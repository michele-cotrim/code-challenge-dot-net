using Newtonsoft.Json;

namespace QuaveChallenge.API.Contracts
{
    public class CompanyBreakdownResponse
    {
        [JsonProperty(PropertyName = "company")]
        public string Company { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }
    }
}
