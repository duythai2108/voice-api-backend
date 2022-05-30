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
    public class FavouriteJobRepository : BaseRepository<FavouriteJob>, IFavouriteJobRepository
    {
        private readonly VoiceAPIDbContext _context;

        public FavouriteJobRepository(VoiceAPIDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<FavouriteJob> GetByCandidateIdAndJobId(Guid candidateId, Guid jobId)
        {
            var targetFavouriteJob = await Get()
                .Where(tempFavouriteJob => tempFavouriteJob.CandidateId.CompareTo(candidateId) == 0
                                            && tempFavouriteJob.JobId.CompareTo(jobId) == 0)
                .FirstOrDefaultAsync();

            return targetFavouriteJob;
        }
    }
}
