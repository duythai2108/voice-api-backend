using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.DbContextVoiceAPI;
using VoiceAPI.Entities;
using VoiceAPI.IRepositories;
using VoiceAPI.Repository;

namespace VoiceAPI.Repositories
{
    public class JobRepository : BaseRepository<Job>, IJobRepository
    {
        private readonly VoiceAPIDbContext _context;

        public JobRepository(VoiceAPIDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<decimal> GetAvailableBalanceOfEnterprise(Guid enterpriseId)
        {
            var wallet = await _context.Enterprises.Join(_context.Accounts,
                                            enterprise => enterprise.Id,
                                            account => account.Id,
                                            (enterprise, account) => new { enterprise = enterprise, account = account })
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
    }
}
