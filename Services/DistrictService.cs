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
using VoiceAPI.Models.Payload.Districts;
using VoiceAPI.Models.Responses.Districts;

namespace VoiceAPI.Services
{
    public class DistrictService : IDistrictService
    {
        private readonly IMapper _mapper;

        private readonly IDistrictRepository _districtRepository;

        private readonly IProvinceService _provinceService;

        public DistrictService(IMapper mapper, 
            IDistrictRepository districtRepository, 
            IProvinceService provinceService)
        {
            _mapper = mapper;

            _districtRepository = districtRepository;

            _provinceService = provinceService;
        }

        public async Task<GenericResult<List<DistrictDTO>>> CreateAllNew(List<DistrictCreatePayload> payloads)
        {
            List<DistrictDTO> response = new();

            var isCodeDuplicate = payloads.GroupBy(tempDistrict => tempDistrict.Code)
                                            .Any(tempDistrict => tempDistrict.Count() > 1);

            var isNameDuplicate = payloads.GroupBy(tempDistrict => tempDistrict.Name)
                                            .Any(tempDistrict => tempDistrict.Count() > 1);

            if (isCodeDuplicate || isNameDuplicate)
                return GenericResult<List<DistrictDTO>>.Error((int)HttpStatusCode.BadRequest,
                                            "V400_34",
                                            "District's Code & Name must be unique.");

            await _districtRepository.RemoveAll();

            foreach (var payload in payloads)
            {
                var targetProvince = await _provinceService.GetById(payload.ProvinceCode);

                if (targetProvince.Data == null)
                    return GenericResult<List<DistrictDTO>>.Error((int)HttpStatusCode.NotFound,
                                            "Province is not found.");

                var targetDistrict = _mapper.Map<District>(payload);

                _districtRepository.Create(targetDistrict);

                response.Add(_mapper.Map<DistrictDTO>(targetDistrict));
            }

             await _districtRepository.SaveAsync();

            return GenericResult<List<DistrictDTO>>.Success(response);
        }

        public async Task<GenericResult<List<DistrictDTO>>> GetAll()
        {
            var districts = await _districtRepository.Get().AsNoTracking()
                                                            .ToListAsync();

            var response = _mapper.Map<List<DistrictDTO>>(districts);

            return GenericResult<List<DistrictDTO>>.Success(response);
        }

        public async Task<GenericResult<DistrictDTO>> GetById(string id)
        {
            var district = await _districtRepository.GetById(id);

            if (district == null)
                return GenericResult<DistrictDTO>.Error((int)HttpStatusCode.NotFound,
                                            "District is not found.");

            var response = _mapper.Map<DistrictDTO>(district);

            return GenericResult<DistrictDTO>.Success(response);
        }
    }
}
