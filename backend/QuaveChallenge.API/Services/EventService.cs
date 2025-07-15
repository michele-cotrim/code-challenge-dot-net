using QuaveChallenge.API.Models;
using QuaveChallenge.API.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using QuaveChallenge.API.Contracts;

namespace QuaveChallenge.API.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EventService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CommunityResponse>> GetCommunitiesAsync()
        {
            var community = await _context.Communities.ToListAsync();
            return _mapper.Map<IEnumerable<CommunityResponse>>(community);
        }

        public async Task<IEnumerable<PersonResponse>> GetPeopleByEventAsync(int communityId)
        {
            var people = await _context.People
                           .Where(x => x.CommunityId == communityId)
                           .OrderBy(x => x.FirstName)
                           .ToListAsync();

            return _mapper.Map<IEnumerable<PersonResponse>>(people);
        }

        public async Task CheckInPersonAsync(int personId, int communityId)
        {
            var person = await _context.People.FirstAsync(x => x.Id == personId);
            var community = await _context.Communities.FirstAsync(x => x.Id == communityId);
            var checkInfo = new CheckinInformation()
            {
                PersonId = personId,
                Person = person,
                CheckinTime = DateTime.UtcNow,
                CheckoutTime = null
            };

            await _context.CheckinInformation.AddAsync(checkInfo);

            _context.SaveChanges();
        }

        public async Task CheckOutPersonAsync(int personId, int communityId)
        {
            var checkInfo = await _context.CheckinInformation.FirstAsync(x => x.PersonId == personId && x.Person.CommunityId == communityId);
            if(checkInfo != null && checkInfo.CheckinTime != null)
            {
                checkInfo.CheckoutTime = DateTime.UtcNow;
            }
            
            _context.CheckinInformation.Update(checkInfo);

            _context.SaveChanges();
        }

        public async Task<bool> AllowCheckOutPersonAsync(int personId, int communityId)
        {
            var checkInfo = await _context.CheckinInformation.FirstAsync(x => x.PersonId == personId && x.Person.CommunityId == communityId);
            
            if (checkInfo != null && DateTime.UtcNow.AddSeconds(-5) > checkInfo.CheckinTime && checkInfo.CheckoutTime == null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> AllowCheckInPersonAsync(int personId, int communityId)
        {
            var checkInfo = await _context.CheckinInformation.FirstOrDefaultAsync(x => x.PersonId == personId && x.Person.CommunityId == communityId);

            if (checkInfo == null)
            {
                return true;
            }

            return false;
        }

        public async Task<EventSummaryResponse> GetEventSummaryAsync(int communityId)
        {
            // Busca todas as pessoas registradas para o evento
            var allPeopleInCommunity = await _context.People
                                                     .Where(p => p.CommunityId == communityId)
                                                     .ToListAsync();

            var totalPeople = allPeopleInCommunity.Count;

            // Busca todos os registros de CheckinInformation para este evento
            var allCheckinRecords = await _context.CheckinInformation
                                                  .Where(ci => ci.Person.CommunityId == communityId)
                                                  .ToListAsync();

            // --- Calcular status de check-in atual ---
            var currentlyCheckedInPeopleIds = new HashSet<int>();
            foreach (var person in allPeopleInCommunity)
            {
                var lastRecord = allCheckinRecords
                    .Where(ci => ci.PersonId == person.Id)
                    .OrderByDescending(ci => ci.CheckinTime)
                    .FirstOrDefault();

                if (lastRecord != null && (!lastRecord.CheckoutTime.HasValue || lastRecord.CheckinTime > lastRecord.CheckoutTime))
                {
                    currentlyCheckedInPeopleIds.Add(person.Id);
                }
            }

            var currentAttendeeCount = currentlyCheckedInPeopleIds.Count;
            var peopleNotCheckedIn = totalPeople - currentAttendeeCount;

            // --- Calcular datas de primeiro/último check-in/checkout do EVENTO ---
            var allEventCheckinTimes = allCheckinRecords.Select(ci => ci.CheckinTime);
            var allEventCheckoutTimes = allCheckinRecords.Where(ci => ci.CheckoutTime.HasValue).Select(ci => ci.CheckoutTime!.Value);

            var firstCheckin = allEventCheckinTimes.Any() ? allEventCheckinTimes.Min() : (DateTime?)null;
            var lastCheckin = allEventCheckinTimes.Any() ? allEventCheckinTimes.Max() : (DateTime?)null;
            var firstCheckout = allEventCheckoutTimes.Any() ? allEventCheckoutTimes.Min() : (DateTime?)null;
            var lastCheckout = allEventCheckoutTimes.Any() ? allEventCheckoutTimes.Max() : (DateTime?)null;


            // --- Calcular detalhamento por empresa para os ATUALMENTE checked-in ---
            var currentlyCheckedInPeople = allPeopleInCommunity
                .Where(p => currentlyCheckedInPeopleIds.Contains(p.Id))
                .ToList();

            var companyBreakdown = currentlyCheckedInPeople
                .GroupBy(p => string.IsNullOrWhiteSpace(p.CompanyName) ? "Não Informado" : p.CompanyName)
                .Select(g => new CompanyBreakdown
                {
                    Company = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .ToList();

            var summary = new EventSummary
            {
                CommunityId = communityId,
                FirstCheckin = firstCheckin,
                LastCheckin = lastCheckin,
                FirstCheckout = firstCheckout,
                LastCheckout = lastCheckout,
                CurrentAttendeeCount = currentAttendeeCount,
                PeopleNotCheckedIn = peopleNotCheckedIn,
                CompanyBreakdown = companyBreakdown
            };

            return _mapper.Map<EventSummaryResponse>(summary);
   
        }
    }
} 