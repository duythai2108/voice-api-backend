using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.DbContextVoiceAPI;
using VoiceAPI.Entities;
using VoiceAPI.IRepositories;
using VoiceAPI.Repository;

namespace VoiceAPI.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly VoiceAPIDbContext _context;

        public CategoryRepository(VoiceAPIDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllCategoryWithSubCategories()
        {
            var categories = await _context.Categories
                                .AsNoTracking()
                                .Include(category => category.SubCategories)
                                .ToListAsync();

            return categories;
        }

        public async Task<Category> GetCategoryByIdWithSubCategories(Guid id)
        {
            var category = await _context.Categories
                                .AsNoTracking()
                                .Include(category => category.SubCategories)
                                .FirstOrDefaultAsync(category => category.Id.CompareTo(id) == 0);

            return category;
        }

        public async Task<List<SubCategory>> GetSubCategoriesByCategoryId(Guid id)
        {
            var categories = await _context.Categories.Join(_context.SubCategories,
                                            category => category.Id,
                                            subCategory => subCategory.CategoryId,
                                            (category, subCategory) => new { category = category, subCategory = subCategory })
                                    .Where(result => result.category.Id.CompareTo(id) == 0
                                                            && result.category.Id.CompareTo(result.subCategory.CategoryId) == 0)
                                    .Select(result => result.subCategory)
                                    .ToListAsync();

            return categories;
        }
    }
}
