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
using VoiceAPI.Models.Payload.SubCategories;
using VoiceAPI.Models.Responses.SubCategories;

namespace VoiceAPI.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly IMapper _mapper;

        private readonly ISubCategoryRepository _subCategoryRepository;

        private readonly ICategoryService _categoryService;

        public SubCategoryService(IMapper mapper, 
            ISubCategoryRepository subCategoryRepository, 
            ICategoryService categoryService)
        {
            _mapper = mapper;

            _subCategoryRepository = subCategoryRepository;
            _categoryService = categoryService;
        }

        public async Task<GenericResult<SubCategoryDTO>> CreateNew(SubCategoryCreatePayload payload)
        {
            var targetSubCategory = _mapper.Map<SubCategory>(payload);

            var targetCategory = await _categoryService.GetById(payload.CategoryId);

            if (targetCategory.Data == null)
                return GenericResult<SubCategoryDTO>.Error((int)HttpStatusCode.NotFound,
                                        "Category is not found.");

            try
            {
                _subCategoryRepository.Create(targetSubCategory);
                await _subCategoryRepository.SaveAsync();

                var response = _mapper.Map<SubCategoryDTO>(targetSubCategory);

                return GenericResult<SubCategoryDTO>.Success(response);
            } catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("duplicate key value violates unique constraint")
                        && ex.InnerException.Message.Contains("SubCategory_Name"))
                {
                    return GenericResult<SubCategoryDTO>.Error((int)HttpStatusCode.Forbidden,
                                            "V400_17",
                                            "This SubCategoryName has existed yet.");
                } else
                {
                    return GenericResult<SubCategoryDTO>.Error((int)HttpStatusCode.InternalServerError,
                                            "Something went wrong.");
                }
            }
        }

        public async Task<GenericResult<List<SubCategoryDTO>>> GetAll()
        {
            var targetSubCategories = await _subCategoryRepository.Get().ToListAsync();

            var response = _mapper.Map<List<SubCategoryDTO>>(targetSubCategories);

            return GenericResult<List<SubCategoryDTO>>.Success(response);
        }

        public async Task<GenericResult<SubCategoryDTO>> GetById(Guid id)
        {
            var targetSubCategory = await _subCategoryRepository.GetById(id);

            if (targetSubCategory == null)
                return GenericResult<SubCategoryDTO>.Error((int)HttpStatusCode.NotFound,
                                        "SubCategory is not found.");

            var response = _mapper.Map<SubCategoryDTO>(targetSubCategory);

            return GenericResult<SubCategoryDTO>.Success(response);
        }
    }
}
