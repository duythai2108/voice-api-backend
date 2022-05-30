using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Models.Common;
using VoiceAPI.Models.Data.Enterprises;
using VoiceAPI.Models.Payload.Enterprises;
using VoiceAPI.Models.Responses.Enterprises;

namespace VoiceAPI.IServices
{
    public interface IEnterpriseService
    {
        Task<GenericResult<EnterpriseDTO>> GetById(Guid id);
        Task<GenericResult<EnterpriseDTO>> UpdateProfile(EnterpriseUpdateDataModel dataModel);
        Task<GenericResult<EnterpriseDTO>> CreateNew(EnterpriseCreatePayload payload);
    }
}
