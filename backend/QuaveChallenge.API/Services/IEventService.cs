using System.Collections.Generic;
using QuaveChallenge.API.Contracts;
using QuaveChallenge.API.Models;

namespace QuaveChallenge.API.Services
{
    public interface IEventService
    {
        Task<IEnumerable<CommunityResponse>> GetCommunitiesAsync();
        Task<IEnumerable<PersonResponse>> GetPeopleByEventAsync(int communityId);
        Task CheckInPersonAsync(int personId, int communityId);
        Task CheckOutPersonAsync(int personId, int communityId);
        Task<bool> AllowCheckOutPersonAsync(int personId, int communityId);
        Task<bool> AllowCheckInPersonAsync(int personId, int communityId);

        Task<EventSummaryResponse> GetEventSummaryAsync(int communityId);
    }
} 