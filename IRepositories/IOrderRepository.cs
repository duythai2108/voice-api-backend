using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities;
using VoiceAPI.IRepository;

namespace VoiceAPI.IRepositories
{
    public interface IOrderRepository : IBaseRepository<Order> 
    {
        Task<List<Order>> GetAllWithJobId(Guid id);
        Task<Order> GetByCandidateIdAndJobId(Guid candidateId, Guid jobId);
        Task<Enterprise> CheckIfJobIsBelongToEnterprise(Guid enterpriseId, Guid jobId);
        Task<Order> GetByIdAndJobId(Guid id, Guid jobId);
        Task<decimal> GetAvailableBalanceOfCandidate(Guid candidateId);
        Task<Job> GetJobByJobId(Guid jobId);
    }
}
