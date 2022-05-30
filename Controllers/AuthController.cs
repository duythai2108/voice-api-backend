using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities;
using VoiceAPI.Entities.Enums;
using VoiceAPI.Helpers;
using VoiceAPI.IServices;
using VoiceAPI.Models.Common;
using VoiceAPI.Models.Payload.Auths;
using VoiceAPI.Models.Responses.Accounts;
using VoiceAPI.Models.Responses.Auths;

namespace VoiceAPI.Controllers
{
    [Route("api/v{version:apiVersion}/auths")]
    [ApiController]
    [ApiVersion("1")]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITools _tools;

        private readonly IAuthService _authService;

        public AuthController(IMapper mapper, ITools tools,  
            IAuthService authService)
        {
            _mapper = mapper;
            _tools = tools;

            _authService = authService;
        }

        [HttpPost("login")]
        [MapToApiVersion("1")]
        public async Task<IActionResult> LoginByPassword(LoginByPasswordPayload payload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.LoginByPassword(payload);

            // Return with statusCode=200 and data if success
            if (result.IsSuccess)
                return Ok(new SingleObjectResponse<JwtTokenDTO>(result.Data));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }

        [HttpPost("admin-login")]
        [MapToApiVersion("1")]
        public async Task<IActionResult> AdminLoginByPassword(AdminLoginByPasswordPayload payload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.AdminLoginByPassword(payload);

            // Return with statusCode=200 and data if success
            if (result.IsSuccess)
                return Ok(new SingleObjectResponse<JwtTokenAdminDTO>(result.Data));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }

        [HttpGet("me")]
        [MapToApiVersion("1")]
        [Authorize]
        public async Task<IActionResult> GetMe()
        {
            var id = _tools.GetUserOfRequest(User.Claims);

            var result = await _authService.GetMe(Guid.Parse(id));

            return Ok(new SingleObjectResponse<AccountDTO>(result.Data));
        }
    }
}
