using Microsoft.EntityFrameworkCore;
using QuaveChallenge.API.Models;
using System.Text.Json;

namespace QuaveChallenge.API.Data.Seeding
{
    public class InitialDataSeeder : IDataSeeder
    {
        private readonly ApplicationDbContext _context;

        public InitialDataSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (await _context.Communities.AnyAsync())
            {
                return; // Data already seeded
            }

            // Add communities
            var communities = new[]
            {
                new Community { Name = "Challenge", CreatedAt = DateTime.UtcNow },
                new Community { Name = "Great Code", CreatedAt = DateTime.UtcNow },
                new Community { Name = "I love code", CreatedAt = DateTime.UtcNow }
            };

            await _context.Communities.AddRangeAsync(communities);
            await _context.SaveChangesAsync();

            // Read people data from JSON file
            var peopleData = await LoadPeopleDataAsync();
            var communityIds = await _context.Communities.Select(c => c.Id).ToListAsync();
            var idx = 0;

            foreach (var personData in peopleData.People)
            {
                var person = new Person
                {
                    FirstName = personData.FirstName,
                    LastName = personData.LastName,
                    CompanyName = personData.CompanyName,
                    Title = personData.Title,
                    CommunityId = communityIds[idx++ % communityIds.Count]
                };

                await _context.People.AddAsync(person);
            }

            await _context.SaveChangesAsync();
        }

        private static async Task<PeopleDataRoot> LoadPeopleDataAsync()
        {
            var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Seeding", "people-data.json");
            var jsonString = await File.ReadAllTextAsync(jsonPath);
            return JsonSerializer.Deserialize<PeopleDataRoot>(jsonString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        private class PeopleDataRoot
        {
            public PersonData[] People { get; set; }
        }

        private class PersonData
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string CompanyName { get; set; }
            public string Title { get; set; }
        }
    }
} 