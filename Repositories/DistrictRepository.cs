using System;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.DbContextVoiceAPI;
using VoiceAPI.Entities;
using VoiceAPI.IRepositories;
using VoiceAPI.Repository;

namespace VoiceAPI.Repositories
{
    public class DistrictRepository : BaseRepository<District>, IDistrictRepository 
    {
        private readonly VoiceAPIDbContext _context;

        public DistrictRepository(VoiceAPIDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> RemoveAll()
        {
            try
            {
                _context.Districts.RemoveRange(_context.Set<District>().ToList());

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
