using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Helpers;
using VoiceAPI.IServices;
using VoiceAPI.Models.Common;
using VoiceAPI.Models.Responses.Wallets;

namespace VoiceAPI.Controllers
{
    [Route("api/v{version:apiVersion}/wallets")]
    [ApiController]
    [ApiVersion("1")]
    public class WalletController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITools _tools;

        private readonly IWalletService _walletService;

        public WalletController(IMapper mapper, ITools tools,
            IWalletService walletService)
        {
            _mapper = mapper;
            _tools = tools;

            _walletService = walletService;
        }
    }
}
