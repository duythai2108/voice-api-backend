using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VoiceAPI.Entities;
using VoiceAPI.IRepositories;
using VoiceAPI.IServices;
using VoiceAPI.Models.Common;
using VoiceAPI.Models.Payload.Categories;
using VoiceAPI.Models.Responses.Categories;

namespace VoiceAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;

        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(IMapper mapper, 
            ICategoryRepository categoryRepository)
        {
            _mapper = mapper;

            _categoryRepository = categoryRepository;
        }

        public async Task<GenericResult<CategoryDTO>> CreateNew(CategoryCreatePayload payload)
        {
            var targetCategory = _mapper.Map<Category>(payload);

            try
            {
                _categoryRepository.Create(targetCategory);
                await _categoryRepository.SaveAsync();
            } catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("duplicate key value violates unique constraint") 
                        && ex.InnerException.Message.Contains("Category_Name")) 
                {
                    return GenericResult<CategoryDTO>.Error((int)HttpStatusCode.Forbidden,
                                            "V400_13",
                                            "This CategoryName has existed yet.");
                }
            }

            var response = _mapper.Map<CategoryDTO>(targetCategory);

            return GenericResult<CategoryDTO>.Success(response);
        }

        public async Task<GenericResult<List<CategoryDetailDTO>>> GetAllWithSubCategories()
        {
            var categories = await _categoryRepository.GetAllCategoryWithSubCategories();

            if (categories == null)
                return GenericResult<List<CategoryDetailDTO>>.Error((int)HttpStatusCode.NoContent, 
                            "V204_01", 
                            "Category table is currently empty.");

            var response = _mapper.Map<List<CategoryDetailDTO>>(categories);

            return GenericResult<List<CategoryDetailDTO>>.Success(response);
        }

        public async Task<GenericResult<CategoryDetailDTO>> GetById(Guid id)
        {
            var category = await _categoryRepository.GetCategoryByIdWithSubCategories(id);

            if (category == null)
                return GenericResult<CategoryDetailDTO>.Error((int)HttpStatusCode.NotFound,
                            "Category is not found.");

            var response = _mapper.Map<CategoryDetailDTO>(category);

            return GenericResult<CategoryDetailDTO>.Success(response);
        }
    }
}
