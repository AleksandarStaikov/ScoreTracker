using AutoMapper;
using ScoreTracker.Common.Models.Game;
using ScoreTracker.Common.Models.Team;
using ScoreTracker.Common.Models.User;
using ScoreTracker.Data.Entities.Game;
using ScoreTracker.Data.Entities.Team;
using ScoreTracker.Data.Entities.User;

namespace ScoreTracker.Data;

public class DataLayerMapping : Profile
{
    public DataLayerMapping()
    {
        CreateMap<UserEntity, User>().ReverseMap(); 

        CreateMap<UserOverviewEntity, UserOverview>().ReverseMap();
        CreateMap<UserEntity, UserOverview>()
            .ForMember(d => d.Username, opt => opt.MapFrom(s => s.Username))
            .ForMember(d => d.AuthId, opt => opt.MapFrom(s => s.AuthId));

        CreateMap<TeamEntity, Team>().ReverseMap();
        CreateMap<TeamOverviewEntity, TeamOverview>().ReverseMap();
        CreateMap<Team, TeamOverview>()
            .ForMember(d => d.RedWins, opt => opt.MapFrom(s => s.RedWins))
            .ForMember(d => d.BlackWins, opt => opt.MapFrom(s => s.BlackWins))
            .ForMember(d => d.ClubsUsername, opt => opt.MapFrom(s => s.ClubsUsername))
            .ForMember(d => d.DiamodsUsername, opt => opt.MapFrom(s => s.DiamodsUsername))
            .ForMember(d => d.HeartsUsername, opt => opt.MapFrom(s => s.HeartsUsername))
            .ForMember(d => d.SpadesUsername, opt => opt.MapFrom(s => s.SpadesUsername))
            .ForMember(d => d.TeamId, opt => opt.MapFrom(s => s.TeamId));

        CreateMap<GameEntity, Game>().ReverseMap();
        CreateMap<GameOverviewEntity, GameOverview>().ReverseMap();
        CreateMap<Game, GameOverview>()
            .ForMember(d => d.GameId, opt => opt.MapFrom(d => d.GameId))
            .ForMember(d => d.TotalBlackPoints, opt => opt.MapFrom(d => d.TotalBlackPoints))
            .ForMember(d => d.TotalRedPoints, opt => opt.MapFrom(d => d.TotalRedPoints));



    }
}