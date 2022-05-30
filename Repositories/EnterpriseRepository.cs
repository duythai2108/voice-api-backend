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
    public class EnterpriseRepository : BaseRepository<Enterprise>, IEnterpriseRepository
    {
        private readonly VoiceAPIDbContext _context;

        public EnterpriseRepository(VoiceAPIDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Account> UpdateAccountStatusAfterCreateProfile(Guid accountId)
        {
            var response = await _context.Enterprises.Join(_context.Accounts,
                                            enterprise => enterprise.Id,
                                            account => account.Id,
                                            (enterprise, account) => new { enterprise, account })
                                    .Select(result => result.account)
                                    .FirstOrDefaultAsync();

            response.Status = Entities.Enums.AccountStatusEnum.ACTIVE;

            _context.Accounts.Update(response);
            await _context.SaveChangesAsync();

            return response;
        }
    }
}
