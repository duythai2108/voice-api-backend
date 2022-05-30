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
using VoiceAPI.Models.Data.JobInvitations;
using VoiceAPI.Models.Data.Orders;
using VoiceAPI.Models.Data.TransactionHistories;
using VoiceAPI.Models.Responses.JobInvitations;
using VoiceAPI.Models.Responses.Jobs;
using VoiceAPI.Models.Responses.Orders;
using VoiceAPI.Models.Responses.Wallets;

namespace VoiceAPI.Services
{
    public class JobInvitationService : IJobInvitationService
    {
        private readonly IMapper _mapper;

        private readonly IJobInvitationRepository _jobInvitationRepository;
        private readonly IOrderRepository _orderRepository;

        private readonly IWalletService _walletService;
        private readonly ITransactionHistoryService _transactionHistoryService;
        private readonly ICandidateService _candidateService;

        public JobInvitationService(IMapper mapper, 
            IJobInvitationRepository jobInvitationRepository,
            IWalletService walletService, 
            ITransactionHistoryService transactionHistoryService, 
            IOrderRepository orderRepository, 
            ICandidateService candidateService)
        {
            _mapper = mapper;

            _jobInvitationRepository = jobInvitationRepository;
            _orderRepository = orderRepository;

            _walletService = walletService;
            _transactionHistoryService = transactionHistoryService;
            _candidateService = candidateService;
        }

        public async Task<GenericResult<WalletCandidateReplyInvitationDTO>> CandidateReplyJobInvitation(JobInvitationCandidateReplyInvitationDataModel dataModel)
        {
            var targetInvitation = await _jobInvitationRepository.Get().AsNoTracking()
                                        .FirstOrDefaultAsync(tempInvitation => tempInvitation.Id.CompareTo(dataModel.Id) == 0);

            if (targetInvitation == null)
                return GenericResult<WalletCandidateReplyInvitationDTO>.Error((int)HttpStatusCode.NotFound,
                                            "JobInvitation is not found.");

            if (targetInvitation.CandidateId.CompareTo(dataModel.CandidateId) != 0)
                return GenericResult<WalletCandidateReplyInvitationDTO>.Error((int)HttpStatusCode.Forbidden,
                                            "V400_30",
                                            "Cannot accept other's JobInvitation.");

            targetInvitation = _mapper.Map<JobInvitation>(dataModel);

            var targetWallet = await _walletService.GetById(dataModel.CandidateId);

            if (targetWallet == null)
                return GenericResult<WalletCandidateReplyInvitationDTO>.Error((int)HttpStatusCode.Forbidden,
                                            "V400_22",
                                            "This Candidate's Wallet has not initialized yet.");

            var targetJob = await _jobInvitationRepository.GetJobByJobInvitationId(targetInvitation.Id);

            if (targetWallet.Data.AvailableBalance < decimal.Multiply(targetJob.Price, 0.1m))
                return GenericResult<WalletCandidateReplyInvitationDTO>.Error((int)HttpStatusCode.Forbidden,
                                            "V400_31",
                                            "AvailableBalance is not enough to accept this invitation.");

            targetInvitation.UpdatedTime = DateTime.UtcNow;

            _jobInvitationRepository.Update(targetInvitation);
            await _jobInvitationRepository.SaveAsync();

            if (targetInvitation.Status == Entities.Enums.JobInvitationStatusEnum.NOT_ACCEPTED)
            {
                var response = _mapper.Map<WalletCandidateReplyInvitationDTO>(targetWallet.Data);
                response.JobInvitation = _mapper.Map<JobInvitationWithJobDetailDTO>(targetInvitation);

                response.JobInvitation.Job = _mapper.Map<JobDTO>(targetJob);

                return GenericResult<WalletCandidateReplyInvitationDTO>.Success(response);
            } 
            else if (targetInvitation.Status == Entities.Enums.JobInvitationStatusEnum.ACCEPTED)
            {
                // Action for Job
                targetJob = await _jobInvitationRepository.UpdateJobStatus(targetJob.Id, Entities.Enums.JobStatusEnum.PROCESSING);

                // Action for Order
                var targetOrder = new Order
                {
                    CandidateId = dataModel.CandidateId, 
                    CreatedTime = DateTime.UtcNow, 
                    JobId = targetJob.Id, 
                    Status = Entities.Enums.OrderStatusEnum.PROCESSING
                };

                // Change status of Candidate to unavailable
                await _candidateService.UpdateStatus(dataModel.CandidateId, Entities.Enums.WorkingStatusEnum.UNAVAILABLE);

                _orderRepository.Create(targetOrder);
                await _orderRepository.SaveAsync();

                var acceptInvitationDataModel = new TransactionHistoryCandidateAcceptInvitationDataModel
                {
                    JobId = targetJob.Id,
                    WalletId = dataModel.CandidateId,
                    Amount = targetJob.Price
                };

                // Action for TransactionHistory & Wallet
                var response = await _transactionHistoryService.AcceptJobInvitation(acceptInvitationDataModel);

                response.Data.Transaction.Order = _mapper.Map<OrderCandidateReplyInvitationDTO>(targetOrder);

                response.Data.JobInvitation = _mapper.Map<JobInvitationWithJobDetailDTO>(targetInvitation);
                response.Data.JobInvitation.Job = _mapper.Map<JobDTO>(targetJob);

                return response;
            } else
            {
                return GenericResult<WalletCandidateReplyInvitationDTO>.Error((int)HttpStatusCode.BadRequest,
                                                        "V400_32",
                                                        "This JobInvitationStatus is not supported or cannot be used in this api.");
            }
        }

        public async Task<GenericResult<JobInvitationDTO>> CreateNew(JobInvitationCreateDataModel dataModel)
        {
            var targetJobInvitation = await _jobInvitationRepository.GetById(dataModel.Id);

            if (targetJobInvitation != null)
                return GenericResult<JobInvitationDTO>.Error((int)HttpStatusCode.Forbidden,
                                    "V400_29",
                                    "Cannot invite more than 1 Candidate for this Job.");

            targetJobInvitation = _mapper.Map<JobInvitation>(dataModel);
            targetJobInvitation.CreatedTime = DateTime.UtcNow;
            targetJobInvitation.Status = Entities.Enums.JobInvitationStatusEnum.PENDING;

            _jobInvitationRepository.Create(targetJobInvitation);
            await _jobInvitationRepository.SaveAsync();

            var response = _mapper.Map<JobInvitationDTO>(targetJobInvitation);

            return GenericResult<JobInvitationDTO>.Success(response);
        }

        public async Task<GenericResult<JobInvitationDTO>> GetById(Guid id)
        {
            var targetJobInvitation = await _jobInvitationRepository.GetById(id);

            if (targetJobInvitation == null)
                return GenericResult<JobInvitationDTO>.Error((int)HttpStatusCode.NotFound,
                                    "JobInvitation is not found.");

            var response = _mapper.Map<JobInvitationDTO>(targetJobInvitation);

            return GenericResult<JobInvitationDTO>.Success(response);
        }
    }
}
