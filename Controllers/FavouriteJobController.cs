using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using VoiceAPI.Helpers;
using VoiceAPI.IServices;
using VoiceAPI.Models.Common;
using VoiceAPI.Models.Data.FavouriteJobs;
using VoiceAPI.Models.Payload.FavouriteJobs;
using VoiceAPI.Models.Responses.FavouriteJobs;

namespace VoiceAPI.Controllers
{
    [Route("api/v{version:apiVersion}/favourite-jobs")]
    [ApiController]
    [ApiVersion("1")]
    public class FavouriteJobController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITools _tools;

        private readonly IFavouriteJobService _favouriteJobService;

        public FavouriteJobController(IMapper mapper, ITools tools,
            IFavouriteJobService favouriteJobService)
        {
            _mapper = mapper;
            _tools = tools;

            _favouriteJobService = favouriteJobService;
        }

        [HttpGet]
        [MapToApiVersion("1")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _favouriteJobService.GetAll();

            // Return with statusCode=200 and data if success
            if (result.IsSuccess)
                return Ok(new MultiObjectResponse<FavouriteJobDTO>(result.Data));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }

        [HttpGet("{favouriteJobId}")]
        [MapToApiVersion("1")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetById(Guid favouriteJobId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _favouriteJobService.GetById(favouriteJobId);

            // Return with statusCode=200 and data if success
            if (result.IsSuccess)
                return Ok(new SingleObjectResponse<FavouriteJobDTO>(result.Data));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }

        [HttpGet("candidates")]
        [MapToApiVersion("1")]
        [Authorize(Roles = "CANDIDATE")]
        public async Task<IActionResult> GetAllByCandidateId()
        {
            var id = _tools.GetUserOfRequest(User.Claims);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _favouriteJobService.GetAllByCandidateId(Guid.Parse(id));

            // Return with statusCode=200 and data if success
            if (result.IsSuccess)
                return Ok(new MultiObjectResponse<FavouriteJobDTO>(result.Data));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }

        [HttpPost]
        [MapToApiVersion("1")]
        [Authorize(Roles = "CANDIDATE")]
        public async Task<IActionResult> CreateNew(FavouriteJobCreatePayload payload)
        {
            var id = _tools.GetUserOfRequest(User.Claims);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataModel = new FavouriteJobCreateDataModel
            {
               CandidateId = Guid.Parse(id), 
               JobId = payload.JobId, 
               CreatedTime = DateTime.UtcNow
            };

            var result = await _favouriteJobService.CreateNew(dataModel);

            // Return with statusCode=201 and data if success
            if (result.IsSuccess)
                return StatusCode((int)HttpStatusCode.Created, 
                            new SingleObjectResponse<FavouriteJobCreateDTO>(result.Data));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }

        [HttpDelete]
        [MapToApiVersion("1")]
        [Authorize(Roles = "CANDIDATE")]
        public async Task<IActionResult> Remove(FavouriteJobRemovePayload payload)
        {
            var id = _tools.GetUserOfRequest(User.Claims);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataModel = new FavouriteJobRemoveDataModel
            {
                CandidateId = Guid.Parse(id),
                JobId = payload.JobId
            };

            var result = await _favouriteJobService.Remove(dataModel);

            // Return with statusCode=201 and data if success
            if (result.IsSuccess)
                return Ok(new SingleObjectResponse<FavouriteJobDTO>(result.Data));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }
    }
}
