using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VoiceAPI.Helpers;
using VoiceAPI.IServices;
using VoiceAPI.Models.Common;
using VoiceAPI.Models.Data.JobInvitations;
using VoiceAPI.Models.Data.Jobs;
using VoiceAPI.Models.Payload.JobInvitations;
using VoiceAPI.Models.Payload.Jobs;
using VoiceAPI.Models.Responses.Jobs;
using VoiceAPI.Models.Responses.Wallets;

namespace VoiceAPI.Controllers
{
    [Route("api/v{version:apiVersion}/jobs")]
    [ApiController]
    [ApiVersion("1")]
    public class JobController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITools _tools;

        private readonly IJobService _jobService;

        public JobController(IMapper mapper, ITools tools, 
            IJobService jobService)
        {
            _mapper = mapper;
            _tools = tools;

            _jobService = jobService;
        }

        [HttpPost]
        [MapToApiVersion("1")]
        [Authorize(Roles = "ENTERPRISE")]
        public async Task<IActionResult> CreateJob(JobEnterprisePostJobPayload payload)
        {
            var id = _tools.GetUserOfRequest(User.Claims);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataModel = _mapper.Map<JobEnterprisePostJobDataModel>(payload);
            dataModel.EnterpriseId = Guid.Parse(id);

            var result = await _jobService.EnterprisePostJob(dataModel);

            // Return with statusCode=201 and data if success
            if (result.IsSuccess)
                return StatusCode((int)HttpStatusCode.Created, 
                            new SingleObjectResponse<WalletEnterprisePostJobDTO>(result.Data));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }

        [HttpPost("invite-candidate")]
        [MapToApiVersion("1")]
        [Authorize(Roles = "ENTERPRISE")]
        public async Task<IActionResult> InviteCandidateForWorking(JobInvitationJobCreatePayload payload)
        {
            var id = _tools.GetUserOfRequest(User.Claims);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jobDataModel = _mapper.Map<JobEnterpriseInviteCandidateForWorkingDataModel>(payload.JobPayload);
            jobDataModel.EnterpriseId = Guid.Parse(id);

            var dataModel = new JobInvitationJobCreateDataModel
            {
                CandidateId = payload.CandidateId, 
                JobDataModel = jobDataModel
            };

            var result = await _jobService.EnterpriseInviteCandidateForWorking(dataModel);

            // Return with statusCode=201 and data if success
            if (result.IsSuccess)
                return StatusCode((int)HttpStatusCode.Created,
                            new SingleObjectResponse<WalletEnterpriseInviteCandidateForWorkingDTO>(result.Data));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }
    }
}
