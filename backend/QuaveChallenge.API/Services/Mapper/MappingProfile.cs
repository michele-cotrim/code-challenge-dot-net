using AutoMapper;
using QuaveChallenge.API.Contracts;
using QuaveChallenge.API.Models;

namespace QuaveChallenge.API.Services.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonResponse>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")); // Mapeia 'Name' para 'FullName'

            CreateMap<Community, CommunityResponse>();
            CreateMap<CompanyBreakdown, CompanyBreakdownResponse>();
            CreateMap<EventSummary, EventSummaryResponse>();


        }
    }
}
