namespace QuaveChallenge.API.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? CompanyName { get; set; }
        public string? Title { get; set; }
        public int? CommunityId { get; set; }
        public Community? Community { get; set; }

    }
} 