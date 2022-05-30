using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Models.Common;
using VoiceAPI.Models.Payload.Districts;
using VoiceAPI.Models.Responses.Districts;

namespace VoiceAPI.IServices
{
    public interface IDistrictService
    {
        Task<GenericResult<List<DistrictDTO>>> CreateAllNew(List<DistrictCreatePayload> payloads);
        Task<GenericResult<DistrictDTO>> GetById(string id);
        Task<GenericResult<List<DistrictDTO>>> GetAll();
    }
}
