using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VoiceAPI.IRepositories;
using VoiceAPI.IServices;
using VoiceAPI.Models.Common;
using VoiceAPI.Models.Responses.Accounts;

namespace VoiceAPI.Services
{
    public class AdminService : IAdminService
    {
        private readonly IMapper _mapper;

        private readonly IAccountRepository _accountRepository;

        public AdminService(IMapper mapper, 
            IAccountRepository accountRepository)
        {
            _mapper = mapper;

            _accountRepository = accountRepository;
        }

        public async Task<GenericResult<AccountDTO>> ApproveAccount(Guid id)
        {
            var account = await _accountRepository.GetById(id);

            if (account == null)
                return GenericResult<AccountDTO>.Error((int)HttpStatusCode.NotFound,
                    "Account is not found.");

            if (account.Status == Entities.Enums.AccountStatusEnum.ACTIVE)
                return GenericResult<AccountDTO>.Error("V400_02",
                    "Account has already been approved.");

            if (account.Status == Entities.Enums.AccountStatusEnum.DELETED)
                return GenericResult<AccountDTO>.Error("V400_06",
                    "Cannot approve deleted account.");

            account.Status = Entities.Enums.AccountStatusEnum.ACTIVE;

            _accountRepository.Update(account);
            await _accountRepository.SaveAsync();

            var response = _mapper.Map<AccountDTO>(account);

            return GenericResult<AccountDTO>.Success(response);
        }

        public async Task<GenericResult<AccountDTO>> BlockAccount(Guid id)
        {
            var account = await _accountRepository.GetById(id);

            if (account == null)
                return GenericResult<AccountDTO>.Error((int)HttpStatusCode.NotFound,
                    "Account is not found.");

            if (account.Status == Entities.Enums.AccountStatusEnum.BLOCKED)
                return GenericResult<AccountDTO>.Error("V400_03",
                    "Account has already been blocked.");

            if (account.Status == Entities.Enums.AccountStatusEnum.DELETED)
                return GenericResult<AccountDTO>.Error("V400_04",
                    "Cannot block deleted account.");

            account.Status = Entities.Enums.AccountStatusEnum.BLOCKED;

            _accountRepository.Update(account);
            await _accountRepository.SaveAsync();

            var response = _mapper.Map<AccountDTO>(account);

            return GenericResult<AccountDTO>.Success(response);
        }

        public async Task<GenericResult<AccountDTO>> DeleteAccount(Guid id)
        {
            var account = await _accountRepository.GetById(id);

            if (account == null)
                return GenericResult<AccountDTO>.Error((int)HttpStatusCode.NotFound,
                    "Account is not found.");

            if (account.Status == Entities.Enums.AccountStatusEnum.DELETED)
                return GenericResult<AccountDTO>.Error("V400_05",
                    "Account has already been deleted.");

            account.Status = Entities.Enums.AccountStatusEnum.DELETED;

            _accountRepository.Update(account);
            await _accountRepository.SaveAsync();

            var response = _mapper.Map<AccountDTO>(account);

            return GenericResult<AccountDTO>.Success(response);
        }
    }
}
