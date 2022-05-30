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
using VoiceAPI.Models.Data.JobInvitations;
using VoiceAPI.Models.Payload.JobInvitations;
using VoiceAPI.Models.Responses.Wallets;

namespace VoiceAPI.Controllers
{
    [Route("api/v{version:apiVersion}/job-invitations")]
    [ApiController]
    [ApiVersion("1")]
    public class JobInvitationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITools _tools;

        private readonly IJobInvitationService _jobInvitationService;

        public JobInvitationController(IMapper mapper, ITools tools, 
            IJobInvitationService jobInvitationService)
        {
            _mapper = mapper;
            _tools = tools;

            _jobInvitationService = jobInvitationService;
        }

        [HttpPut("reply")]
        [MapToApiVersion("1")]
        [Authorize(Roles = "CANDIDATE")]
        public async Task<IActionResult> ReplyJobInvitation(JobInvitationCandidateReplyInvitationPayload payload)
        {
            var id = _tools.GetUserOfRequest(User.Claims);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataModel = _mapper.Map<JobInvitationCandidateReplyInvitationDataModel>(payload);
            dataModel.CandidateId = Guid.Parse(id);

            var result = await _jobInvitationService.CandidateReplyJobInvitation(dataModel);

            // Return with statusCode=200 and data if success
            if (result.IsSuccess)
                return Ok(new SingleObjectResponse<WalletCandidateReplyInvitationDTO>(result.Data));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }
    }
}
