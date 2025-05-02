using System.Threading.Tasks;

namespace QuaveChallenge.API.Data.Seeding
{
    public interface IDataSeeder
    {
        Task SeedAsync();
    }
} 