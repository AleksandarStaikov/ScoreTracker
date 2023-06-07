using AutoMapper;
using ScoreTracker.WebApi.Models.Game;
using ScoreTracker.WebApi.Models.Team;
using ScoreTracker.Common.Models.Game;
using ScoreTracker.Common.Models.Team;

namespace ScoreTracker.WebApi;

public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        CreateMap<CreateTeamModel, CreateTeamDto>()
            .ForMember(d => d.SpadesUsername, opt => opt.MapFrom(s => s.SpadesUsername))
            .ForMember(d => d.HeartsUsername, opt => opt.MapFrom(s => s.HeartsUsername))
            .ForMember(d => d.DiamondsUsername, opt => opt.MapFrom(s => s.DiamondsUsername));

        CreateMap<CreateGameModel, CreateGameDto>()
            .ForMember(d => d.TeamId, opt => opt.MapFrom(s => s.TeamId))
            .ForMember(d => d.RedPoints, opt => opt.MapFrom(s => s.RedPoints))
            .ForMember(d => d.BlackPoints, opt => opt.MapFrom(s => s.BlackPoints));
    }
}