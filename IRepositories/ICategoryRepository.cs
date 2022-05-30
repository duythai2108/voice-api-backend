using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities;
using VoiceAPI.IRepository;

namespace VoiceAPI.IRepositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<List<SubCategory>> GetSubCategoriesByCategoryId(Guid id);
        Task<List<Category>> GetAllCategoryWithSubCategories();
        Task<Category> GetCategoryByIdWithSubCategories(Guid id);
    }
}
