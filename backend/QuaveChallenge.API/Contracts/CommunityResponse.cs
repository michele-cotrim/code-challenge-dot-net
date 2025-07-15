using Newtonsoft.Json;

namespace QuaveChallenge.API.Contracts
{
    public class CommunityResponse
    {
        [JsonProperty(PropertyName  = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}
