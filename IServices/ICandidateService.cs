using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities.Enums;
using VoiceAPI.Models.Common;
using VoiceAPI.Models.Data.Candidates;
using VoiceAPI.Models.Payload.Candidates;
using VoiceAPI.Models.Responses.Candidates;

namespace VoiceAPI.IServices
{
    public interface ICandidateService
    {
        Task<GenericResult<CandidateDTO>> GetById(Guid id);
        Task<GenericResult<CandidateDTO>> UpdateProfile(CandidateUpdateDataModel dataModel);
        Task<GenericResult<CandidateDTO>> CreateNew(CandidateCreatePayload payload);
        Task<GenericResult<CandidateDTO>> UpdateStatus(Guid id, WorkingStatusEnum status);
    }
}
