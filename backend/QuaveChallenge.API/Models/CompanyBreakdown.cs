namespace QuaveChallenge.API.Models
{
    public class CompanyBreakdown
    {
        /// <summary>
        /// O nome da empresa.
        /// </summary>
        public string Company { get; set; } = string.Empty;

        /// <summary>
        /// O número de participantes associados a esta empresa.
        /// </summary>
        public int Count { get; set; }
    }
}
