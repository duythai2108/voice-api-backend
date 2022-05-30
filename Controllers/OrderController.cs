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
using VoiceAPI.Models.Data.Orders;
using VoiceAPI.Models.Payload.Orders;
using VoiceAPI.Models.Responses.Orders;
using VoiceAPI.Models.Responses.Wallets;

namespace VoiceAPI.Controllers
{
    [Route("api/v{version:apiVersion}/orders")]
    [ApiController]
    [ApiVersion("1")]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITools _tools;

        private readonly IOrderService _orderService;

        public OrderController(IMapper mapper, ITools tools, 
            IOrderService orderService)
        {
            _mapper = mapper;
            _tools = tools;

            _orderService = orderService;
        }

        [HttpPost]
        [MapToApiVersion("1")]
        [Authorize(Roles = "CANDIDATE")]
        public async Task<IActionResult> CandidateApplyForJob(OrderCandidateApplyForJobPayload payload)
        {
            var id = _tools.GetUserOfRequest(User.Claims);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataModel = new OrderCandidateApplyForJobDataModel
            {
                CandidateId = Guid.Parse(id),
                Payload = payload
            };

            var result = await _orderService.CandidateApplyForJob(dataModel);

            // Return with statusCode=201 and data if success
            if (result.IsSuccess)
                return StatusCode((int)HttpStatusCode.Created, 
                        new SingleObjectResponse<WalletCandidateApplyJobDTO>(result.Data));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }

        [HttpPut("approve")]
        [MapToApiVersion("1")]
        [Authorize(Roles = "ENTERPRISE")]
        public async Task<IActionResult> EnterpriseApproveJob(OrderEnterpriseApproveJobPayload payload)
        {
            var id = _tools.GetUserOfRequest(User.Claims);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataModel = new OrderEnterpriseApproveJobDataModel
            {
                EnterpriseId = Guid.Parse(id),
                Payload = payload
            };

            var result = await _orderService.EnterpriseApproveJob(dataModel);

            // Return with statusCode=200 and data if success
            if (result.IsSuccess)
                return Ok(new SingleObjectResponse<OrderEnterpriseApproveJobDTO>(result.Data));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }

        [HttpPut("finish")]
        [MapToApiVersion("1")]
        [Authorize(Roles = "ENTERPRISE")]
        public async Task<IActionResult> EnterpriseFinishOrder(OrderEnterpriseFinishJobPayload payload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _orderService.FinishOrder(payload.OrderId);

            // Return with statusCode=200 and data if success
            if (result.IsSuccess)
                return Ok(new SingleObjectResponse<WalletOrderFinishDTO>(result.Data));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }
    }
}
