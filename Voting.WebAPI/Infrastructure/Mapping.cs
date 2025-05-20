using AutoMapper;
using Voting.DataAccess.Models;
using Voting.Shared.DTOs;

namespace Voting.WebAPI.Infrastructure
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Poll, PollDto>();
            CreateMap<Vote, VoteDto>();
            CreateMap<Answer, AnswerDto>();

            CreateMap<UserDto, User>(MemberList.Source)
            .ForSourceMember(dest => dest.Id, opt => opt.DoNotValidate())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForSourceMember(src => src.Password, opt => opt.DoNotValidate())
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<User, UserDto>(MemberList.Destination)
                .ForMember(src => src.Password, opt => opt.Ignore());
        }

    }
}
    

