using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VoiceAPI.IRepositories;
using VoiceAPI.IServices;
using VoiceAPI.Models.Common;
using VoiceAPI.Models.Responses.Wallets;

namespace VoiceAPI.Services
{
    public class WalletService : IWalletService
    {
        private readonly IMapper _mapper;

        private readonly IWalletRepository _walletRepository;

        public WalletService(IMapper mapper, 
            IWalletRepository walletRepository)
        {
            _mapper = mapper;

            _walletRepository = walletRepository;
        }

        public async Task<GenericResult<List<WalletWithTransactionsDTO>>> GetAll()
        {
            var targetWallets = await _walletRepository.GetAllWalletWithTransactions();

            var response = _mapper.Map<List<WalletWithTransactionsDTO>>(targetWallets);

            return GenericResult<List<WalletWithTransactionsDTO>>.Success(response);
        }

        public async Task<GenericResult<WalletWithTransactionsDTO>> GetById(Guid id)
        {
            var targetWallet = await _walletRepository.GetWalletWithTransactions(id);

            if (targetWallet == null)
                return GenericResult<WalletWithTransactionsDTO>.Error((int)HttpStatusCode.NotFound,
                                        "Wallet is not found.");

            var response = _mapper.Map<WalletWithTransactionsDTO>(targetWallet);

            return GenericResult<WalletWithTransactionsDTO>.Success(response);
        }
    }
}
