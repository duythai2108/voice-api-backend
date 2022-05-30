using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Models.Common;
using VoiceAPI.Models.Data.FavouriteJobs;
using VoiceAPI.Models.Responses.FavouriteJobs;

namespace VoiceAPI.IServices
{
    public interface IFavouriteJobService
    {
        Task<GenericResult<FavouriteJobDTO>> GetById(Guid id);
        Task<GenericResult<List<FavouriteJobDTO>>> GetAll();
        Task<GenericResult<List<FavouriteJobDTO>>> GetAllByCandidateId(Guid candidateId);
        Task<GenericResult<FavouriteJobCreateDTO>> CreateNew(FavouriteJobCreateDataModel dataModel);
        Task<GenericResult<FavouriteJobDTO>> Remove(FavouriteJobRemoveDataModel dataModel);
    }
}
