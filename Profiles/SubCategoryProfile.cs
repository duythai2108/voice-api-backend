using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities;
using VoiceAPI.Models.Payload.SubCategories;
using VoiceAPI.Models.Responses.SubCategories;

namespace VoiceAPI.Profiles
{
    public class SubCategoryProfile : Profile
    {
        public SubCategoryProfile()
        {
            CreateMap<SubCategory, SubCategoryDTO>().ReverseMap();

            CreateMap<SubCategory, SubCategoryCreatePayload>().ReverseMap();
        }
    }
}
