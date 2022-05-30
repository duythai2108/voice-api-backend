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
    public class ProvinceRepository : BaseRepository<Province>, IProvinceRepository
    {
        private readonly VoiceAPIDbContext _context;

        public ProvinceRepository(VoiceAPIDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> RemoveAll()
        {
            try
            {
                _context.Provinces.RemoveRange(_context.Set<Province>().ToList());
                await _context.SaveChangesAsync();

                return true;
            } catch
            {
                return false;
            }
        }
    }
}
