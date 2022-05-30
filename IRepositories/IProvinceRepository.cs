using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities;
using VoiceAPI.IRepository;

namespace VoiceAPI.IRepositories
{
    public interface IProvinceRepository : IBaseRepository<Province>
    {
        Task<bool> RemoveAll();
    }
}
