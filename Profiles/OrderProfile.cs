using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities;
using VoiceAPI.Models.Data.Orders;
using VoiceAPI.Models.Responses.Orders;

namespace VoiceAPI.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDTO>().ReverseMap();

            CreateMap<Order, OrderCandidateApplyJobDTO>().ReverseMap();

            CreateMap<Order, OrderEnterpriseApproveJobDTO>().ReverseMap();

            CreateMap<Order, OrderFinishDTO>().ReverseMap();

            CreateMap<Order, OrderCandidateReplyInvitationDTO>().ReverseMap();
        }
    }
}
