using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VoiceAPI.Entities;
using VoiceAPI.Entities.Enums;
using VoiceAPI.IRepositories;
using VoiceAPI.IServices;
using VoiceAPI.Models.Common;
using VoiceAPI.Models.Payload.Accounts;
using VoiceAPI.Models.Responses.Accounts;

namespace VoiceAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;

        private readonly IAccountRepository _accountRepository;

        public AccountService(IMapper mapper, 
            IAccountRepository accountRepository)
        {
            _mapper = mapper;

            _accountRepository = accountRepository;
        }

        public async Task<GenericResult<AccountDTO>> CreateAccount(AccountCreatePayload payload)
        {
            var targetAccount = _mapper.Map<Account>(payload);

            targetAccount.Status = Entities.Enums.AccountStatusEnum.INACTIVE;

            targetAccount.CreatedTime = DateTime.UtcNow;

            _accountRepository.Create(targetAccount);
            await _accountRepository.SaveAsync();

            var response = _mapper.Map<AccountDTO>(targetAccount);

            return GenericResult<AccountDTO>.Success(response);
        }

        public async Task<GenericResult<AccountDTO>> GetById(Guid id)
        {
            var account = await _accountRepository.GetById(id);

            if (account == null)
                return GenericResult<AccountDTO>.Error((int)HttpStatusCode.NotFound,
                    "Account is not found.");

            var response = _mapper.Map<AccountDTO>(account);

            return GenericResult<AccountDTO>.Success(response);
        }

        public async Task<GenericResult<AccountDTO>> UpdateAccount(AccountUpdatePayload payload)
        {
            var account = await _accountRepository.GetById(payload.Id);

            if (account == null)
                return GenericResult<AccountDTO>.Error((int)HttpStatusCode.NotFound,
                    "Account is not found.");

            account = _mapper.Map<Account>(payload);

            _accountRepository.Update(account);
            await _accountRepository.SaveAsync();

            var response = _mapper.Map<AccountDTO>(account);

            return GenericResult<AccountDTO>.Success(response);
        }

        public async Task<GenericResult<AccountDTO>> UpdateAccountStatus(Guid id, AccountStatusEnum status)
        {
            var targetAccount = await _accountRepository.GetById(id);

            targetAccount.Status = status;

            _accountRepository.Update(targetAccount);
            await _accountRepository.SaveAsync();

            var response = _mapper.Map<AccountDTO>(targetAccount);

            return GenericResult<AccountDTO>.Success(response);
        }
    }
}
