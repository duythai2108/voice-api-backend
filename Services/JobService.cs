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
using VoiceAPI.Models.Data.JobInvitations;
using VoiceAPI.Models.Data.Jobs;
using VoiceAPI.Models.Data.TransactionHistories;
using VoiceAPI.Models.Responses.JobInvitations;
using VoiceAPI.Models.Responses.Jobs;
using VoiceAPI.Models.Responses.Wallets;

namespace VoiceAPI.Services
{
    public class JobService : IJobService
    {
        private readonly IMapper _mapper;

        private readonly IJobRepository _jobRepository;

        private readonly ISubCategoryService _subCategoryService;
        private readonly ITransactionHistoryService _transactionHistoryService;
        private readonly IJobInvitationService _jobInvitationService;
        private readonly ICandidateService _candidateService;

        public JobService(IMapper mapper, 
            IJobRepository jobRepository, 
            ISubCategoryService subCategoryService, 
            ITransactionHistoryService transactionHistoryService, 
            IJobInvitationService jobInvitationService, 
            ICandidateService candidateService)
        {
            _mapper = mapper;

            _jobRepository = jobRepository;

            _subCategoryService = subCategoryService;
            _transactionHistoryService = transactionHistoryService;
            _jobInvitationService = jobInvitationService;
            _candidateService = candidateService;
        }

        public async Task<GenericResult<WalletEnterpriseInviteCandidateForWorkingDTO>> 
                    EnterpriseInviteCandidateForWorking(JobInvitationJobCreateDataModel dataModel)
        {
            var enterpriseAvailableBalance =
                            await _jobRepository.GetAvailableBalanceOfEnterprise(dataModel.JobDataModel.EnterpriseId);

            if (enterpriseAvailableBalance == -1)
                return GenericResult<WalletEnterpriseInviteCandidateForWorkingDTO>.Error((int)HttpStatusCode.Forbidden,
                                        "V400_21",
                                        "This Enterprise's Wallet has not initialized yet.");

            if (enterpriseAvailableBalance < dataModel.JobDataModel.Price)
                return GenericResult<WalletEnterpriseInviteCandidateForWorkingDTO>.Error((int)HttpStatusCode.Forbidden,
                                        "V400_28",
                                        "AvailableBalance is not enough to invite Candidate for working this Job.");

            var targetJob = _mapper.Map<Job>(dataModel.JobDataModel);

            if ((targetJob.MinuteDuration == null || targetJob.MinuteDuration == 0)
                    && (targetJob.DayDuration == null || targetJob.DayDuration == 0)
                    && (targetJob.HourDuration == null || targetJob.HourDuration == 0))
                return GenericResult<WalletEnterpriseInviteCandidateForWorkingDTO>.Error((int)HttpStatusCode.Forbidden,
                                    "V400_24",
                                    "Must be at least 1 duration.");

            if (targetJob.MinuteDuration == 0)
                targetJob.MinuteDuration = null;

            if (targetJob.DayDuration == 0)
                targetJob.DayDuration = null;

            if (targetJob.HourDuration == 0)
                targetJob.HourDuration = null;

            var subCategory = await _subCategoryService.GetById(dataModel.JobDataModel.SubCategoryId);

            if (!subCategory.IsSuccess)
                return GenericResult<WalletEnterpriseInviteCandidateForWorkingDTO>.Error((int)HttpStatusCode.NotFound,
                                    "SubCategory is not found.");

            var targetCandidate = await _candidateService.GetById(dataModel.CandidateId);

            if (targetCandidate.Data == null)
                return GenericResult<WalletEnterpriseInviteCandidateForWorkingDTO>.Error((int)HttpStatusCode.NotFound,
                                                "Candidate is not found.");

            targetJob.IsPublic = false;

            _jobRepository.Create(targetJob);
            await _jobRepository.SaveAsync();

            // Action for JobInvitation

            var jobInvitationDataModel = new JobInvitationCreateDataModel
            {
                Id = targetJob.Id,
                CandidateId = dataModel.CandidateId
            };

            var targetJobInvitation = await _jobInvitationService.CreateNew(jobInvitationDataModel);

            // Action for Wallet
            var targetTransactionDataModel = new TransactionHistoryEnterpriseInviteCandidateForWorkingDataModel
            {
                WalletId = dataModel.JobDataModel.EnterpriseId,
                JobId = targetJob.Id,
                Amount = dataModel.JobDataModel.Price
            };

            var response = await _transactionHistoryService.InviteCandidateForWorking(targetTransactionDataModel);

            response.Data.Transaction.Job.JobInvitation = _mapper.Map<JobInvitationDTO>(targetJobInvitation.Data);

            return GenericResult<WalletEnterpriseInviteCandidateForWorkingDTO>.Success(response.Data);
        }

        public async Task<GenericResult<WalletEnterprisePostJobDTO>> EnterprisePostJob(JobEnterprisePostJobDataModel dataModel)
        {
            var enterpriseAvailableBalance = 
                            await _jobRepository.GetAvailableBalanceOfEnterprise(dataModel.EnterpriseId);

            if (enterpriseAvailableBalance == -1) 
                return GenericResult<WalletEnterprisePostJobDTO>.Error((int)HttpStatusCode.Forbidden,
                                        "V400_21",
                                        "This Enterprise's Wallet has not initialized yet.");

            if (enterpriseAvailableBalance < dataModel.Price)
                return GenericResult<WalletEnterprisePostJobDTO>.Error((int)HttpStatusCode.Forbidden,
                                        "V400_19",
                                        "AvailableBalance is not enough to post this Job.");

            var targetJob = _mapper.Map<Job>(dataModel);

            if ((targetJob.MinuteDuration == null || targetJob.MinuteDuration == 0)
                    && (targetJob.DayDuration == null || targetJob.DayDuration == 0)
                    && (targetJob.HourDuration == null || targetJob.HourDuration == 0))
                return GenericResult<WalletEnterprisePostJobDTO>.Error((int)HttpStatusCode.Forbidden,
                                    "V400_24",
                                    "Must be at least 1 duration.");

            if (targetJob.MinuteDuration == 0)
                targetJob.MinuteDuration = null;

            if (targetJob.DayDuration == 0)
                targetJob.DayDuration = null;

            if (targetJob.HourDuration == 0)
                targetJob.HourDuration = null;

            var subCategory = await _subCategoryService.GetById(dataModel.SubCategoryId);

            if (!subCategory.IsSuccess)
                return GenericResult<WalletEnterprisePostJobDTO>.Error((int)HttpStatusCode.NotFound,
                                    "SubCategory is not found.");

            _jobRepository.Create(targetJob);
            await _jobRepository.SaveAsync();

            // Action for TransactionHistory & Wallet

            var targetTransactionHistoryDataModel = new TransactionHistoryEnterprisePostJobDataModel
            {
                WalletId = dataModel.EnterpriseId, 
                JobId = targetJob.Id, 
                Amount = dataModel.Price
            };

            var response = await _transactionHistoryService.EnterprisePostJob(targetTransactionHistoryDataModel);

            return GenericResult<WalletEnterprisePostJobDTO>.Success(response.Data);
        }

        public async Task<GenericResult<JobDTO>> GetById(Guid id)
        {
            var targetJob = await _jobRepository.GetById(id);

            if (targetJob == null)
                return GenericResult<JobDTO>.Error((int)HttpStatusCode.NotFound,
                                    "Job is not found.");

            var response = _mapper.Map<JobDTO>(targetJob);

            return GenericResult<JobDTO>.Success(response);
        }

        public async Task<GenericResult<JobDTO>> UpdateStatusAfterJobApproved(Guid id)
        {
            var targetJob = await _jobRepository.GetById(id);

            if (targetJob == null)
                return GenericResult<JobDTO>.Error((int)HttpStatusCode.NotFound,
                                    "Job is not found.");

            targetJob.JobStatus = Entities.Enums.JobStatusEnum.PROCESSING;

            _jobRepository.Update(targetJob);
            await _jobRepository.SaveAsync();

            var response = _mapper.Map<JobDTO>(targetJob);

            return GenericResult<JobDTO>.Success(response);
        }

        public async Task<GenericResult<JobDTO>> UpdateStatusAfterJobFinished(Guid id)
        {
            var targetJob = await _jobRepository.GetById(id);

            if (targetJob == null)
                return GenericResult<JobDTO>.Error((int)HttpStatusCode.NotFound,
                                    "Job is not found.");

            targetJob.JobStatus = Entities.Enums.JobStatusEnum.FINISHED;

            _jobRepository.Update(targetJob);
            await _jobRepository.SaveAsync();

            var response = _mapper.Map<JobDTO>(targetJob);

            return GenericResult<JobDTO>.Success(response);
        }
    }
}
