using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Models.Common;
using VoiceAPI.Models.Responses.Accounts;
using VoiceAPI.Models.Responses.Enterprises;

namespace VoiceAPI.IServices
{
    public interface IAdminService
    {
        Task<GenericResult<AccountDTO>> ApproveAccount(Guid id);
        Task<GenericResult<AccountDTO>> BlockAccount(Guid id);
        Task<GenericResult<AccountDTO>> DeleteAccount(Guid id);
    }
}
