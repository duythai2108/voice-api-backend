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
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly VoiceAPIDbContext _context;

        public OrderRepository(VoiceAPIDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Enterprise> CheckIfJobIsBelongToEnterprise(Guid enterpriseId, Guid jobId)
        {
            var response = await _context.Orders.Join(_context.Jobs,
                                    order => order.JobId,
                                    job => job.Id,
                                    (order, job) => new { order = order, job = job })
                                .Where(result => result.job.Id.CompareTo(jobId) == 0)
                                .Select(result => result.job).Join(_context.Enterprises,
                                    job => job.EnterpriseId,
                                    enterprise => enterprise.Id,
                                    (job, enterprise) => new { job = job, enterprise = enterprise })
                                .Where(result => result.enterprise.Id.CompareTo(enterpriseId) == 0)
                                .Select(result => result.enterprise)
                                .FirstOrDefaultAsync();

            return response;
        }

        public async Task<List<Order>> GetAllWithJobId(Guid id)
        {
            var orders = await Get()
                            .Where(tempOrder => tempOrder.JobId.CompareTo(id) == 0)
                            .ToListAsync();

            return orders;
        }

        public async Task<decimal> GetAvailableBalanceOfCandidate(Guid candidateId)
        {
            var wallet = await _context.Candidates.Join(_context.Accounts,
                                            candidate => candidate.Id,
                                            account => account.Id,
                                            (candidate, account) => new { candidate, account })
                                    .Select(result => result.account).Join(_context.Wallets,
                                            account => account.Id,
                                            wallet => wallet.Id,
                                            (account, wallet) => new { account = account, wallet = wallet })
                                    .Select(result => result.wallet)
                                    .FirstOrDefaultAsync();
            return wallet != null
                    ? wallet.AvailableBalance
                    : -1;
        }

        public async Task<Order> GetByCandidateIdAndJobId(Guid candidateId, Guid jobId)
        {
            var order = await Get()
                            .Where(tempOrder => tempOrder.CandidateId.CompareTo(candidateId) == 0
                                                    && tempOrder.JobId.CompareTo(jobId) == 0)
                            .FirstOrDefaultAsync();

            return order;
        }

        public async Task<Order> GetByIdAndJobId(Guid id, Guid jobId)
        {
            var order = await Get()
                            .Where(tempOrder => tempOrder.Id.CompareTo(id) == 0
                                    && tempOrder.JobId.CompareTo(jobId) == 0)
                            .FirstOrDefaultAsync();

            return order;
        }

        public async Task<Job> GetJobByJobId(Guid jobId)
        {
            var response = await _context.Orders.Join(_context.Jobs,
                                        order => order.JobId,
                                        job => job.Id,
                                        (order, job) => new { order, job })
                                .Where(result => result.job.Id.CompareTo(jobId) == 0)
                                .Select(result => result.job)
                                .FirstOrDefaultAsync();

            return response;
        }
    }
}
