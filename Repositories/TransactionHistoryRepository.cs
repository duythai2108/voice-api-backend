using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.DbContextVoiceAPI;
using VoiceAPI.Entities;
using VoiceAPI.IRepositories;
using VoiceAPI.Repository;

namespace VoiceAPI.Repositories
{
    public class TransactionHistoryRepository : BaseRepository<TransactionHistory>, ITransactionHistoryRepository
    {
        private readonly VoiceAPIDbContext _context;

        public TransactionHistoryRepository(VoiceAPIDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Job> GetJobByJobId(Guid jobId)
        {
            var response = await _context.TransactionHistories.Join(_context.Jobs,
                                                transactionHistory => transactionHistory.JobId,
                                                job => job.Id,
                                                (transactionHistory, job) => new { transactionHistory = transactionHistory, job = job })
                                        .Where(result => result.job.Id.CompareTo(jobId) == 0)
                                        .Select(result => result.job)
                                        .FirstOrDefaultAsync();

            return response;
        }

        public async Task<Wallet> GetWalletByWalletId(Guid walletId)
        {
            var response = await _context.TransactionHistories.Join(_context.Wallets,
                                                transactionHistory => transactionHistory.WalletId,
                                                wallet => wallet.Id,
                                                (transactionHistory, wallet) => new { transactionHistory = transactionHistory, wallet = wallet })
                                        .Where(result => result.wallet.Id.CompareTo(walletId) == 0)
                                        .Select(result => result.wallet)
                                        .FirstOrDefaultAsync();

            return response;
        }

        public async Task<Wallet> UpdateBalanceAfterDeposit(Guid walletId, decimal balance)
        {
            var response = await _context.TransactionHistories.Join(_context.Wallets,
                                                transactionHistory => transactionHistory.WalletId,
                                                wallet => wallet.Id,
                                                (transactionHistory, wallet) => new { transactionHistory = transactionHistory, wallet = wallet })
                                        .Where(result => result.wallet.Id.CompareTo(walletId) == 0)
                                        .Select(result => result.wallet)
                                        .FirstOrDefaultAsync();

            response.AvailableBalance = decimal.Add(response.AvailableBalance, balance);

            _context.Wallets.Update(response);
            await _context.SaveChangesAsync();

            return response;
        }

        public async Task<Wallet> UpdateBalanceAfterPostJob(Guid walletId, decimal jobPrice)
        {
            var response = await _context.TransactionHistories.Join(_context.Wallets,
                                                transactionHistory => transactionHistory.WalletId,
                                                wallet => wallet.Id,
                                                (transactionHistory, wallet) => new { transactionHistory = transactionHistory, wallet = wallet })
                                        .Where(result => result.wallet.Id.CompareTo(walletId) == 0)
                                        .Select(result => result.wallet)
                                        .FirstOrDefaultAsync();

            response.AvailableBalance = decimal.Subtract(response.AvailableBalance, jobPrice);
            response.LockedBalance = decimal.Add(response.LockedBalance, jobPrice);

            _context.Wallets.Update(response);
            await _context.SaveChangesAsync();

            return response;
        }

        public async Task<Wallet> UpdateBalanceAfterApplyJob(Guid walletId, decimal jobPrice)
        {
            var response = await _context.TransactionHistories.Join(_context.Wallets,
                                                transactionHistory => transactionHistory.WalletId,
                                                wallet => wallet.Id,
                                                (transactionHistory, wallet) => new { transactionHistory, wallet })
                                        .Where(result => result.wallet.Id.CompareTo(walletId) == 0)
                                        .Select(result => result.wallet)
                                        .FirstOrDefaultAsync();

            decimal chargeAmount = decimal.Multiply(jobPrice, 0.1m); // 10% jobPrice charge

            response.AvailableBalance = decimal.Subtract(response.AvailableBalance, chargeAmount);
            response.LockedBalance = decimal.Add(response.LockedBalance, chargeAmount);

            _context.Wallets.Update(response);

            await _context.SaveChangesAsync();

            return response;
        }

        public async Task<Order> GetOrderByCandidateIdAndJobId(Guid candidateId, Guid jobId)
        {
            var response = await _context.TransactionHistories.Join(_context.Jobs,
                                                transactionHistory => transactionHistory.JobId,
                                                job => job.Id,
                                                (transactionHistory, job) => new { transactionHistory, job })
                                        .Where(result => result.job.Id.CompareTo(jobId) == 0)
                                        .Select(result => result.job).Join(_context.Orders,
                                                job => job.Id,
                                                order => order.JobId,
                                                (job, order) => new { job, order })
                                        .Where(result => result.order.CandidateId.CompareTo(candidateId) == 0)
                                        .Select(result => result.order)
                                        .FirstOrDefaultAsync();

            return response;
        }

        public async Task<Order> GetOrderByOrderId(Guid orderId)
        {
            var response = await _context.TransactionHistories.Join(_context.Jobs,
                                                transactionHistory => transactionHistory.JobId,
                                                job => job.Id,
                                                (transactionHistory, job) => new { transactionHistory, job })
                                        .Select(result => result.job).Join(_context.Orders,
                                                job => job.Id,
                                                order => order.JobId,
                                                (job, order) => new { job, order })
                                        .Where(result => result.order.Id.CompareTo(orderId) == 0)
                                        .Select(result => result.order)
                                        .FirstOrDefaultAsync();

            return response;
        }

        public async Task<List<Candidate>> GetCandidatesAppliedJobByOrderId(Guid orderId)
        {
            var targetJob = await GetJobByOrderId(orderId);

            var response = await _context.TransactionHistories.Join(_context.Jobs,
                                                transactionHistory => transactionHistory.JobId,
                                                job => job.Id,
                                                (transactionHistory, job) => new { transactionHistory, job })
                                        .Select(result => result.job).Join(_context.Orders,
                                                job => job.Id,
                                                order => order.JobId,
                                                (job, order) => new { job, order })
                                        .Select(result => result.order).Join(_context.Candidates,
                                                order => order.CandidateId,
                                                candidate => candidate.Id,
                                                (order, candidate) => new { order, candidate })
                                        .Where(result => result.order.JobId.CompareTo(targetJob.Id) == 0)
                                        .Select(result => result.candidate)
                                        .ToListAsync();

            return response;
        }

        public async Task<Job> GetJobByOrderId(Guid orderId)
        {
            var response = await _context.TransactionHistories.Join(_context.Jobs,
                                                transactionHistory => transactionHistory.JobId,
                                                job => job.Id,
                                                (transactionHistory, job) => new { transactionHistory, job })
                                        .Select(result => result.job).Join(_context.Orders,
                                                job => job.Id,
                                                order => order.JobId,
                                                (job, order) => new { job, order })
                                        .Where(result => result.order.Id.CompareTo(orderId) == 0)
                                        .Select(result => result.job)
                                        .FirstOrDefaultAsync();

            return response;
        }

        public async Task<Candidate> GetCandidateByOrderId(Guid orderId)
        {
            var response = await _context.TransactionHistories.Join(_context.Jobs,
                                                transactionHistory => transactionHistory.JobId,
                                                job => job.Id,
                                                (transactionHistory, job) => new { transactionHistory, job })
                                        .Select(result => result.job).Join(_context.Orders,
                                                job => job.Id,
                                                order => order.JobId,
                                                (job, order) => new { job, order })
                                        .Where(result => result.order.Id.CompareTo(orderId) == 0)
                                        .Select(result => result.order).Join(_context.Candidates,
                                                order => order.CandidateId,
                                                candidate => candidate.Id,
                                                (order, candidate) => new { order, candidate })
                                        .Select(result => result.candidate)
                                        .FirstOrDefaultAsync();

            return response;
        }

        public async Task<List<Order>> GetAllOrderApplyToJobByOrderId(Guid orderId)
        {
            var targetJob = await GetJobByOrderId(orderId);

            var response = await _context.TransactionHistories.Join(_context.Jobs,
                                                transactionHistory => transactionHistory.JobId,
                                                job => job.Id,
                                                (transactionHistory, job) => new { transactionHistory, job })
                                        .Where(result => result.job.Id.CompareTo(targetJob.Id) == 0)
                                        .Select(result => result.job).Join(_context.Orders,
                                                job => job.Id,
                                                order => order.JobId,
                                                (job, order) => new { job, order })
                                        .Select(result => result.order)
                                        .ToListAsync();

            return response;
        }

        public async Task<Enterprise> GetEnterpriseByOrderId(Guid orderId)
        {
            var targetJob = await GetJobByOrderId(orderId);

            var response = await _context.TransactionHistories.Join(_context.Jobs,
                                                transactionHistory => transactionHistory.JobId,
                                                job => job.Id,
                                                (transactionHistory, job) => new { transactionHistory, job })
                                        .Where(result => result.job.Id.CompareTo(targetJob.Id) == 0)
                                        .Select(result => result.job).Join(_context.Enterprises,
                                                job => job.EnterpriseId,
                                                enterprise => enterprise.Id,
                                                (job, enterprise) => new { job, enterprise })
                                        .Select(result => result.enterprise)
                                        .FirstOrDefaultAsync();

            return response;
        }
    }
}
