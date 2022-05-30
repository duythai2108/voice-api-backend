using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities;
using VoiceAPI.Models.Common;
using VoiceAPI.Models.Payload.Auths;
using VoiceAPI.Models.Responses.Accounts;
using VoiceAPI.Models.Responses.Auths;

namespace VoiceAPI.IServices
{
    public interface IAuthService
    {
        JwtTokenDTO GenerateToken(Account account);
        JwtTokenAdminDTO GenerateToken(Admin admin);
        Task<GenericResult<JwtTokenDTO>> LoginByPassword(LoginByPasswordPayload payload);
        Task<GenericResult<JwtTokenAdminDTO>> AdminLoginByPassword(AdminLoginByPasswordPayload payload);
        Task<GenericResult<AccountDTO>> GetMe(Guid id);
        Task<GenericResult<Admin>> GetMeAdmin(Guid id);
    }
}
