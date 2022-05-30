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
using VoiceAPI.Models.Payload.Provinces;
using VoiceAPI.Models.Responses.Provinces;

namespace VoiceAPI.Services
{
    public class ProvinceService : IProvinceService
    {
        private readonly IMapper _mapper;

        private readonly IProvinceRepository _provinceRepository;

        public ProvinceService(IMapper mapper, 
            IProvinceRepository provinceRepository)
        {
            _mapper = mapper;

            _provinceRepository = provinceRepository;
        }

        public async Task<GenericResult<List<ProvinceDTO>>> CreateAllNew(List<ProvinceCreatePayload> payloads)
        {
            List<ProvinceDTO> response = new();

            var isCodeDuplicate = payloads.GroupBy(tempProvince => tempProvince.Code)
                                            .Any(tempProvince => tempProvince.Count() > 1);

            var isNameDuplicate = payloads.GroupBy(tempProvince => tempProvince.Name)
                                            .Any(tempProvince => tempProvince.Count() > 1);

            if (isCodeDuplicate || isNameDuplicate)
                return GenericResult<List<ProvinceDTO>>.Error((int)HttpStatusCode.BadRequest,
                                            "V400_33",
                                            "Province's Code & Name must be unique.");

            await _provinceRepository.RemoveAll();

            foreach (var payload in payloads)
            {
                var targetProvince = _mapper.Map<Province>(payload);

                _provinceRepository.Create(targetProvince);

                response.Add(_mapper.Map<ProvinceDTO>(targetProvince));
            }

            await _provinceRepository.SaveAsync();

            return GenericResult<List<ProvinceDTO>>.Success(response);
        }

        public async Task<GenericResult<List<ProvinceDTO>>> GetAll()
        {
            var provinces = await _provinceRepository.Get().AsNoTracking()
                                                            .ToListAsync();

            var response = _mapper.Map<List<ProvinceDTO>>(provinces);

            return GenericResult<List<ProvinceDTO>>.Success(response);
        }

        public async Task<GenericResult<ProvinceDTO>> GetById(string code)
        {
            var province = await _provinceRepository.GetById(code);

            if (province == null)
                return GenericResult<ProvinceDTO>.Error((int)HttpStatusCode.NotFound,
                                                "Province is not found.");

            var response = _mapper.Map<ProvinceDTO>(province);

            return GenericResult<ProvinceDTO>.Success(response);
        }
    }
}
