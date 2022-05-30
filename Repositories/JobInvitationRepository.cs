using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.DbContextVoiceAPI;
using VoiceAPI.Entities;
using VoiceAPI.Entities.Enums;
using VoiceAPI.IRepositories;
using VoiceAPI.Repository;

namespace VoiceAPI.Repositories
{
    public class JobInvitationRepository : BaseRepository<JobInvitation>, IJobInvitationRepository
    {
        private readonly VoiceAPIDbContext _context;

        public JobInvitationRepository(VoiceAPIDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Job> GetJobByJobInvitationId(Guid id)
        {
            var response = await _context.JobInvitations.AsNoTracking().Join(_context.Jobs,
                                                jobInvitation => jobInvitation.Id,
                                                job => job.Id,
                                                (jobInvitation, job) => new { jobInvitation, job })
                                    .Where(result => result.jobInvitation.Id.CompareTo(id) == 0)
                                    .Select(result => result.job)
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync();

            return response;
        }

        public async Task<Job> UpdateJobStatus(Guid id, JobStatusEnum jobStatus)
        {
            var response = await _context.JobInvitations.Join(_context.Jobs,
                                                jobInvitation => jobInvitation.Id,
                                                job => job.Id,
                                                (jobInvitation, job) => new { jobInvitation, job })
                                    .Where(result => result.jobInvitation.Id.CompareTo(id) == 0)
                                    .Select(result => result.job)
                                    .FirstOrDefaultAsync();

            response.JobStatus = jobStatus;

            _context.Jobs.Update(response);
            await _context.SaveChangesAsync();

            return response;
        }
    }
}
