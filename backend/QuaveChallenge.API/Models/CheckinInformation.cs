namespace QuaveChallenge.API.Models
{
    public class CheckinInformation
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; } = null!;
        public DateTime? CheckinTime { get; set; } = DateTime.UtcNow;
        public DateTime? CheckoutTime { get; set; } = DateTime.UtcNow;
    }
}
