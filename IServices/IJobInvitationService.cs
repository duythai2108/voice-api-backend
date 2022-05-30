using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Models.Common;
using VoiceAPI.Models.Data.JobInvitations;
using VoiceAPI.Models.Responses.JobInvitations;
using VoiceAPI.Models.Responses.Wallets;

namespace VoiceAPI.IServices
{
    public interface IJobInvitationService
    {
        Task<GenericResult<JobInvitationDTO>> GetById(Guid id);
        Task<GenericResult<JobInvitationDTO>> CreateNew(JobInvitationCreateDataModel dataModel);
        Task<GenericResult<WalletCandidateReplyInvitationDTO>> CandidateReplyJobInvitation(JobInvitationCandidateReplyInvitationDataModel dataModel);
    }
}
