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
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(VoiceAPIDbContext context) : base(context)
        {
        }

        public async Task<Account> GetByEmailAndPassword(string email, string password)
        {
            var account = await Get()
                .Where(tempAccount => tempAccount.Email.Equals(email)
                                            && tempAccount.Password.Equals(password))
                .FirstOrDefaultAsync();

            return account;
        }

        public async Task<Account> GetByPhoneNumberAndPassword(string phoneNumber, string password)
        {
            var account = await Get()
                .Where(tempAccount => tempAccount.PhoneNumber.Equals(phoneNumber)
                                            && tempAccount.Password.Equals(password))
                .FirstOrDefaultAsync();

            return account;
        }
    }
}
