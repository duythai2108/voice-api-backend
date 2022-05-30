using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Models.Common;
using VoiceAPI.Models.Data.Orders;
using VoiceAPI.Models.Payload.Orders;
using VoiceAPI.Models.Responses.Orders;
using VoiceAPI.Models.Responses.Wallets;

namespace VoiceAPI.IServices
{
    public interface IOrderService
    {
        Task<GenericResult<WalletCandidateApplyJobDTO>> CandidateApplyForJob(OrderCandidateApplyForJobDataModel dataModel);
        Task<GenericResult<OrderEnterpriseApproveJobDTO>> EnterpriseApproveJob(OrderEnterpriseApproveJobDataModel dataModel);
        Task<GenericResult<OrderDTO>> GetById(Guid id);
        Task<GenericResult<OrderDTO>> UpdateStatusAfterJobApproved(Guid id);
        Task<GenericResult<WalletOrderFinishDTO>> FinishOrder(Guid id);
    }
}
