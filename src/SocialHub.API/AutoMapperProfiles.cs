﻿using AutoMapper;
using SocialHub.Domain.Models;
using SocialHub.Infrastructure.Dtos;

namespace SocialHub.Infrastructure
{
    class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Account, AccountDto>();
        }
    }
}
