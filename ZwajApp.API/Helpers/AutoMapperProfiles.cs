﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ZwajApp.API.DTOs;
using ZwajApp.API.Models;

namespace ZwajApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>();
            CreateMap<User, UserForDetailsDto>()
            .ForMember(dest => dest.PhotoURL, opt => { opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url); })
            .ForMember(dest => dest.Age, opt => { opt.ResolveUsing(src => src.DateOfBirth.CalculateAge()); });

            CreateMap<Photo, PhotoForDetailsDto>();

        }
    }
}
