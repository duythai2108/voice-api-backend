using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities;
using VoiceAPI.Models.Common;
using VoiceAPI.Models.Payload.Categories;
using VoiceAPI.Models.Responses.Categories;

namespace VoiceAPI.IServices
{
    public interface ICategoryService
    {
        Task<GenericResult<CategoryDTO>> CreateNew(CategoryCreatePayload payload);
        Task<GenericResult<CategoryDetailDTO>> GetById(Guid id);
        Task<GenericResult<List<CategoryDetailDTO>>> GetAllWithSubCategories();
    }
}
