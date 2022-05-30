using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities;
using VoiceAPI.IRepository;

namespace VoiceAPI.IRepositories
{
    public interface ITransactionHistoryRepository : IBaseRepository<TransactionHistory>
    {
        Task<Wallet> UpdateBalanceAfterDeposit(Guid walletId, decimal balance);
        Task<Wallet> GetWalletByWalletId(Guid walletId);
        Task<Wallet> UpdateBalanceAfterPostJob(Guid walletId, decimal jobPrice);
        Task<Job> GetJobByJobId(Guid jobId);
        Task<Wallet> UpdateBalanceAfterApplyJob(Guid walletId, decimal jobPrice);
        Task<Order> GetOrderByCandidateIdAndJobId(Guid candidateId, Guid jobId);
        Task<Order> GetOrderByOrderId(Guid orderId);
        Task<List<Candidate>> GetCandidatesAppliedJobByOrderId(Guid orderId);
        Task<Job> GetJobByOrderId(Guid orderId);
        Task<Candidate> GetCandidateByOrderId(Guid orderId);
        Task<List<Order>> GetAllOrderApplyToJobByOrderId(Guid orderId);
        Task<Enterprise> GetEnterpriseByOrderId(Guid orderId);
    }
}
