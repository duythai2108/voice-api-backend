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
    public class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        public AdminRepository(VoiceAPIDbContext context) : base(context)
        {
        }

        public async Task<Admin> GetByEmailAndPassword(string email, string password)
        {
            var admin = await Get()
                .Where(tempAdmin => tempAdmin.Email.Equals(email)
                                        && tempAdmin.Password.Equals(password))
                .FirstOrDefaultAsync();

            return admin;
        }
    }
}
