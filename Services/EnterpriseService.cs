using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VoiceAPI.Entities;
using VoiceAPI.IRepositories;
using VoiceAPI.IServices;
using VoiceAPI.Models.Common;
using VoiceAPI.Models.Data.Enterprises;
using VoiceAPI.Models.Payload.Enterprises;
using VoiceAPI.Models.Responses.Enterprises;

namespace VoiceAPI.Services
{
    public class EnterpriseService : IEnterpriseService
    {
        private readonly IMapper _mapper;

        private readonly IEnterpriseRepository _enterpriseRepository;
        private readonly IAccountService _accountService;

        public EnterpriseService(IMapper mapper, 
            IEnterpriseRepository enterpriseRepository, 
            IAccountService accountService)
        {
            _mapper = mapper;

            _enterpriseRepository = enterpriseRepository;
            _accountService = accountService;
        }

        public async Task<GenericResult<EnterpriseDTO>> CreateNew(EnterpriseCreatePayload payload)
        {
            var targetEnterprise = _mapper.Map<Enterprise>(payload);

            _enterpriseRepository.Create(targetEnterprise);
            await _enterpriseRepository.SaveAsync();

            await _enterpriseRepository.UpdateAccountStatusAfterCreateProfile(targetEnterprise.Id);

            var targetAccount = await _accountService.GetById(targetEnterprise.Id);

            await _accountService.UpdateAccountStatus(targetAccount.Data.Id, Entities.Enums.AccountStatusEnum.ACTIVE);

            var response = _mapper.Map<EnterpriseDTO>(targetEnterprise);

            return GenericResult<EnterpriseDTO>.Success(response);
        }

        public async Task<GenericResult<EnterpriseDTO>> GetById(Guid id)
        {
            var targetEnterprise = await _enterpriseRepository.GetById(id);

            var response = _mapper.Map<EnterpriseDTO>(targetEnterprise);

            return GenericResult<EnterpriseDTO>.Success(response);
        }

        public async Task<GenericResult<EnterpriseDTO>> UpdateProfile(EnterpriseUpdateDataModel dataModel)
        {
            var targetEnterprise = await _enterpriseRepository.GetById(dataModel.Id);

            if (targetEnterprise == null)
                return GenericResult<EnterpriseDTO>.Error((int)HttpStatusCode.NotFound,
                    "Enterprise is not existed.");

            _mapper.Map(dataModel, targetEnterprise);

            _enterpriseRepository.Update(targetEnterprise);
            await _enterpriseRepository.SaveAsync();

            var response = _mapper.Map<EnterpriseDTO>(targetEnterprise);

            return GenericResult<EnterpriseDTO>.Success(response);
        }
    }
}
