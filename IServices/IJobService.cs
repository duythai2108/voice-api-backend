using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities.Enums;
using VoiceAPI.Models.Common;
using VoiceAPI.Models.Data.JobInvitations;
using VoiceAPI.Models.Data.Jobs;
using VoiceAPI.Models.Payload.Jobs;
using VoiceAPI.Models.Responses.Jobs;
using VoiceAPI.Models.Responses.Wallets;

namespace VoiceAPI.IServices
{
    public interface IJobService
    {
        Task<GenericResult<WalletEnterprisePostJobDTO>> EnterprisePostJob(JobEnterprisePostJobDataModel dataModel);
        Task<GenericResult<JobDTO>> UpdateStatusAfterJobApproved(Guid id);
        Task<GenericResult<JobDTO>> GetById(Guid id);
        Task<GenericResult<JobDTO>> UpdateStatusAfterJobFinished(Guid id);
        Task<GenericResult<WalletEnterpriseInviteCandidateForWorkingDTO>> EnterpriseInviteCandidateForWorking(JobInvitationJobCreateDataModel dataModel);
    }
}
