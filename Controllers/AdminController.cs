using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VoiceAPI.Entities;
using VoiceAPI.Helpers;
using VoiceAPI.IServices;
using VoiceAPI.Models.Common;
using VoiceAPI.Models.Data.TransactionHistories;
using VoiceAPI.Models.Payload.TransactionHistories;
using VoiceAPI.Models.Responses.Accounts;
using VoiceAPI.Models.Responses.Wallets;

namespace VoiceAPI.Controllers
{
    [Route("api/v{version:apiVersion}/admins")]
    [ApiController]
    [ApiVersion("1")]
    public class AdminController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITools _tools;

        private readonly IAdminService _adminService;
        private readonly IAuthService _authService;
        private readonly ITransactionHistoryService _transactionHistoryService;
        private readonly IWalletService _walletService;

        public AdminController(IMapper mapper, ITools tools, 
            IAdminService adminService, 
            IAuthService authService, 
            ITransactionHistoryService transactionHistoryService, 
            IWalletService walletService)
        {
            _mapper = mapper;
            _tools = tools;

            _adminService = adminService;
            _authService = authService;
            _transactionHistoryService = transactionHistoryService;
            _walletService = walletService;
        }

        //[HttpPut("accounts/approve")]
        //[MapToApiVersion("1")]
        //public async Task<IActionResult> ApproveAccount(Guid id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var result = await _adminService.ApproveAccount(id);

        //    // Return with statusCode=200 and data if success
        //    if (result.IsSuccess)
        //        return Ok(new SingleObjectResponse<AccountDTO>(result.Data));

        //    // Add error response data informations
        //    Response.StatusCode = result.StatusCode;

        //    var response = _mapper.Map<ErrorResponse>(result);

        //    return StatusCode(result.StatusCode, response);
        //}

        [HttpPut("accounts/block")]
        [MapToApiVersion("1")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> BlockAccount(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _adminService.BlockAccount(id);

            // Return with statusCode=200 and data if success
            if (result.IsSuccess)
                return Ok(new SingleObjectResponse<AccountDTO>(result.Data));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }

        [HttpDelete("accounts")]
        [MapToApiVersion("1")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _adminService.DeleteAccount(id);

            // Return with statusCode=200 and data if success
            if (result.IsSuccess)
                return Ok(new SingleObjectResponse<AccountDTO>(result.Data));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }

        [HttpGet("me")]
        [MapToApiVersion("1")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetMeAdmin()
        {
            var id = _tools.GetUserOfRequest(User.Claims);

            var result = await _authService.GetMeAdmin(Guid.Parse(id));

            return Ok(new SingleObjectResponse<Admin>(result.Data));
        }

        [HttpPost("accounts/deposit")]
        [MapToApiVersion("1")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DepositBalanceToAccount(TransactionHistoryAdminCreatePayload payload)
        {
            var id = _tools.GetUserOfRequest(User.Claims);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataModel = new TransactionHistoryAdminCreateDataModel
            {
                AdminId = Guid.Parse(id),
                Payload = payload
            };

            var result = await _transactionHistoryService.AdminDepositBalance(dataModel);

            // Return with statusCode=201 and data if success
            if (result.IsSuccess)
                return StatusCode((int)HttpStatusCode.Created, 
                                new SingleObjectResponse<WalletAdminDepositBalanceDTO>(result.Data));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }

        [HttpGet("wallets/{id}")]
        [MapToApiVersion("1")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetWalletById(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _walletService.GetById(id);

            // Return with statusCode=200 and data if success
            if (result.IsSuccess)
                return Ok(new SingleObjectResponse<WalletWithTransactionsDTO>(result.Data));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }

        [HttpGet("wallets")]
        [MapToApiVersion("1")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAllWallet(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _walletService.GetAll();

            // Return with statusCode=200 and data if success
            if (result.IsSuccess)
                return Ok(new MultiObjectResponse<WalletWithTransactionsDTO>(result.Data));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }
    }
}
