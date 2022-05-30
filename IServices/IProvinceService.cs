using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Models.Common;
using VoiceAPI.Models.Payload.Provinces;
using VoiceAPI.Models.Responses.Provinces;

namespace VoiceAPI.IServices
{
    public interface IProvinceService
    {
        Task<GenericResult<List<ProvinceDTO>>> CreateAllNew(List<ProvinceCreatePayload> payloads);
        Task<GenericResult<ProvinceDTO>> GetById(string code);
        Task<GenericResult<List<ProvinceDTO>>> GetAll();
    }
}
