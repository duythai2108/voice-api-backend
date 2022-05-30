using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities;
using VoiceAPI.IRepository;
using VoiceAPI.Models.Responses.Wallets;

namespace VoiceAPI.IRepositories
{
    public interface IWalletRepository : IBaseRepository<Wallet>
    {
        Task<Wallet> UpdateBalanceAfterRejected(Guid walletId, decimal balance);
        Task<Wallet> GetWalletWithTransactions(Guid id);
        Task<List<Wallet>> GetAllWalletWithTransactions();
        Task<Wallet> UpdateEnterpriseBalanceAfterFinishOrder(Guid walletId, decimal jobPrice);
        Task<Wallet> UpdateCandidateBalanceAfterFinishOrder(Guid walletId, decimal jobPrice);
        Task<Wallet> UpdateBalanceAfterInviteCandidateForWorking(Guid walletId, decimal jobPrice);
        Task<Wallet> UpdateBalanceAfterAcceptInvitation(Guid walletId, decimal jobPrice);
    }
}
