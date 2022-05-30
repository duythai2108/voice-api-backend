using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VoiceAPI.Entities;
using VoiceAPI.Entities.Enums;
using VoiceAPI.IRepositories;
using VoiceAPI.IServices;
using VoiceAPI.Models.Common;
using VoiceAPI.Models.Data.Candidates;
using VoiceAPI.Models.Payload.Candidates;
using VoiceAPI.Models.Responses.Candidates;

namespace VoiceAPI.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly IMapper _mapper;

        private readonly ICandidateRepository _candidateRepository;
        private readonly IAccountService _accountService;

        public CandidateService(IMapper mapper, 
            ICandidateRepository candidateRepository, 
            IAccountService accountService)
        {
            _mapper = mapper;

            _candidateRepository = candidateRepository;
            _accountService = accountService;
        }

        public async Task<GenericResult<CandidateDTO>> CreateNew(CandidateCreatePayload payload)
        {
            var targetCandidate = await _candidateRepository.GetById(payload.Id);

            if (targetCandidate != null)
                return GenericResult<CandidateDTO>.Error("V400_08",
                    "Candidate has existed yet.");

            var targetAccount = await _accountService.GetById(payload.Id);

            if (targetAccount == null)
                return GenericResult<CandidateDTO>.Error("V400_09",
                    "Cannot create Candidate profile for non-exist Account.");

            if (targetAccount.Data.Role != Entities.Enums.RoleEnum.CANDIDATE)
                return GenericResult<CandidateDTO>.Error("V400_08",
                    "Cannot create Candidate profile for Enterprise Account.");

            targetCandidate = _mapper.Map<Candidate>(payload);

            _candidateRepository.Create(targetCandidate);
            await _candidateRepository.SaveAsync();

            await _accountService.UpdateAccountStatus(targetAccount.Data.Id, AccountStatusEnum.ACTIVE);

            var response = _mapper.Map<CandidateDTO>(targetCandidate);

            return GenericResult<CandidateDTO>.Success(response);
        }

        public async Task<GenericResult<CandidateDTO>> GetById(Guid id)
        {
            var targetCandidate = await _candidateRepository.GetById(id);

            if (targetCandidate == null)
                return GenericResult<CandidateDTO>.Error((int)HttpStatusCode.NotFound,
                    "Candidate is not found.");

            var response = _mapper.Map<CandidateDTO>(targetCandidate);

            return GenericResult<CandidateDTO>.Success(response);
        }

        public async Task<GenericResult<CandidateDTO>> UpdateProfile(CandidateUpdateDataModel dataModel)
        {
            var targetCandidate = await _candidateRepository.GetById(dataModel.Id);

            if (targetCandidate == null)
                return GenericResult<CandidateDTO>.Error((int)HttpStatusCode.NotFound,
                    "Candidate is not found.");

            _mapper.Map(dataModel, targetCandidate);

            _candidateRepository.Update(targetCandidate);
            await _candidateRepository.SaveAsync();

            var response = _mapper.Map<CandidateDTO>(targetCandidate);

            return GenericResult<CandidateDTO>.Success(response);
        }

        public async Task<GenericResult<CandidateDTO>> UpdateStatus(Guid id, WorkingStatusEnum status)
        {
            var targetCandidate = await _candidateRepository.GetById(id);

            if (targetCandidate == null)
                return GenericResult<CandidateDTO>.Error((int)HttpStatusCode.NotFound,
                                    "Candidate is not found.");

            targetCandidate.Status = status;
            _candidateRepository.Update(targetCandidate);
            await _candidateRepository.SaveAsync();

            var response = _mapper.Map<CandidateDTO>(targetCandidate);

            return GenericResult<CandidateDTO>.Success(response);
        }
    }
}
