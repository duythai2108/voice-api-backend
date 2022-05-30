using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Models.Common;
using VoiceAPI.Models.Responses.Wallets;

namespace VoiceAPI.IServices
{
    public interface IWalletService
    {
        public Task<GenericResult<WalletWithTransactionsDTO>> GetById(Guid id);
        public Task<GenericResult<List<WalletWithTransactionsDTO>>> GetAll();
    }
}
