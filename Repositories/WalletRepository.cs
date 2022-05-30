using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.DbContextVoiceAPI;
using VoiceAPI.Entities;
using VoiceAPI.IRepositories;
using VoiceAPI.Models.Responses.Wallets;
using VoiceAPI.Repository;

namespace VoiceAPI.Repositories
{
    public class WalletRepository : BaseRepository<Wallet>, IWalletRepository
    {
        private readonly VoiceAPIDbContext _context;

        public WalletRepository(VoiceAPIDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Wallet>> GetAllWalletWithTransactions()
        {
            var wallets = await _context.Wallets.AsNoTracking()
                                    .Include(tempWallet => tempWallet.TransactionHistories)
                                    .ToListAsync();

            return wallets;
        }

        public async Task<Wallet> GetWalletWithTransactions(Guid id)
        {
            var wallet = await _context.Wallets.AsNoTracking()
                                    .Include(tempWallet => tempWallet.TransactionHistories)
                                    .FirstOrDefaultAsync(tempWallet => tempWallet.Id.CompareTo(id) == 0);

            return wallet;
        }

        public async Task<Wallet> UpdateBalanceAfterAcceptInvitation(Guid walletId, decimal jobPrice)
        {
            var wallet = await GetById(walletId);

            var chargeAmount = decimal.Multiply(jobPrice, 0.1m); // 10% of JobPrice

            wallet.AvailableBalance = decimal.Subtract(wallet.AvailableBalance, chargeAmount);
            wallet.LockedBalance = decimal.Add(wallet.LockedBalance, chargeAmount);

            _context.Wallets.Update(wallet);
            await _context.SaveChangesAsync();

            return wallet;
        }

        public async Task<Wallet> UpdateBalanceAfterInviteCandidateForWorking(Guid walletId, decimal jobPrice)
        {
            var response = await GetById(walletId);

            response.AvailableBalance = decimal.Subtract(response.AvailableBalance, jobPrice);
            response.LockedBalance = decimal.Add(response.LockedBalance, jobPrice);

            _context.Wallets.Update(response);
            await _context.SaveChangesAsync();

            return response;
        }

        public async Task<Wallet> UpdateBalanceAfterRejected(Guid walletId, decimal balance)
        {
            var response = await GetById(walletId);

            var refundAmount = decimal.Multiply(balance, 0.1m); // 10% of JobPrice

            response.AvailableBalance = decimal.Add(response.AvailableBalance, refundAmount);
            response.LockedBalance = decimal.Subtract(response.LockedBalance, refundAmount);

            _context.Wallets.Update(response);
            await _context.SaveChangesAsync();

            return response;
        }

        public async Task<Wallet> UpdateCandidateBalanceAfterFinishOrder(Guid walletId, decimal jobPrice)
        {
            var wallet = await GetById(walletId);

            var chargeAmount = decimal.Multiply(jobPrice, 0.1m); // 10% of JobPrice

            wallet.AvailableBalance = decimal.Add(wallet.AvailableBalance, decimal.Multiply(jobPrice, 0.9m));
            wallet.LockedBalance = decimal.Subtract(wallet.LockedBalance, chargeAmount);

            _context.Wallets.Update(wallet);

            return wallet;
        }

        public async Task<Wallet> UpdateEnterpriseBalanceAfterFinishOrder(Guid walletId, decimal jobPrice)
        {
            var wallet = await GetById(walletId);

            wallet.LockedBalance = decimal.Subtract(wallet.LockedBalance, jobPrice);

            _context.Wallets.Update(wallet);

            return wallet;
        }
    }
}
