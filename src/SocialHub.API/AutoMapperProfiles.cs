using AutoMapper;
using LanguageExt.Common;
using SocialHub.API.Dtos;
using SocialHub.API.Models;
using SocialHub.API.Models.Dtos;
using SocialHub.Application.Models;
using SocialHub.Domain.Entities;

namespace SocialHub.API
{
    class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Account, AccountDto>();
            CreateMap<Post, PostDto>();
            CreateMap<Error, ErrorDto>();
            CreateMap<AuthResult, AuthResultDto>();
            CreateMap<AccountProfile, AccountProfileDto>();
        }
    }
}
