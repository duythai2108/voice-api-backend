using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities;
using VoiceAPI.Entities.Enums;
using VoiceAPI.Models.Common;
using VoiceAPI.Models.Payload.Accounts;
using VoiceAPI.Models.Responses.Accounts;

namespace VoiceAPI.IServices
{
    public interface IAccountService
    {
        Task<GenericResult<AccountDTO>> GetById(Guid id);
        Task<GenericResult<AccountDTO>> CreateAccount(AccountCreatePayload payload);
        Task<GenericResult<AccountDTO>> UpdateAccount(AccountUpdatePayload payload);
        Task<GenericResult<AccountDTO>> UpdateAccountStatus(Guid id, AccountStatusEnum status);
    }
}
