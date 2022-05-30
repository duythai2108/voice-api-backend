using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities;
using VoiceAPI.Models.Data.FavouriteJobs;
using VoiceAPI.Models.Responses.FavouriteJobs;

namespace VoiceAPI.Profiles
{
    public class FavouriteProfile : Profile
    {
        public FavouriteProfile()
        {
            CreateMap<FavouriteJob, FavouriteJobDTO>().ReverseMap();
            CreateMap<FavouriteJob, FavouriteJobCreateDTO>().ReverseMap();

            CreateMap<FavouriteJob, FavouriteJobCreateDataModel>().ReverseMap();

            CreateMap<FavouriteJob, FavouriteJobRemoveDataModel>().ReverseMap();
        }
    }
}
