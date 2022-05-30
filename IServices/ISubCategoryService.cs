using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Models.Common;
using VoiceAPI.Models.Payload.SubCategories;
using VoiceAPI.Models.Responses.SubCategories;

namespace VoiceAPI.IServices
{
    public interface ISubCategoryService
    {
        Task<GenericResult<SubCategoryDTO>> CreateNew(SubCategoryCreatePayload payload);
        Task<GenericResult<SubCategoryDTO>> GetById(Guid id);
        Task<GenericResult<List<SubCategoryDTO>>> GetAll();
    }
}
