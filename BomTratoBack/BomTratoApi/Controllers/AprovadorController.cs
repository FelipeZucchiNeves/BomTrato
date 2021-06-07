using BomTratoApp.Models;
using BomTratoApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BomTratoApi.Controllers
{
    [Authorize]
    public class AprovadorController : ApiController
    {
        private readonly IAprovadorService _aprovadorService;
        public AprovadorController(IAprovadorService aprovadorService)
        {
            _aprovadorService = aprovadorService;
        }
        [AllowAnonymous]
        [HttpGet("aprovador-management")]
        public async Task<IEnumerable<AprovadorViewModel>> Get()
        {
            return await _aprovadorService.GetAll();
        }
        [AllowAnonymous]
        [HttpGet("aprovador-management/{id:guid}")]
        public async Task<AprovadorViewModel> Get(Guid id)
        {
            return await _aprovadorService.GetById(id);
        }
        [AllowAnonymous]
        [HttpPost("aprovador-management")]
        public async Task<IActionResult>Post([FromBody] AprovadorViewModel aprovadorViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _aprovadorService.Register(aprovadorViewModel));
        }
        [AllowAnonymous]
        [HttpPut("aprovador-management")]
        public async Task<IActionResult>Put([FromBody] AprovadorViewModel aprovadorViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _aprovadorService.Update(aprovadorViewModel));
        }
        [AllowAnonymous]
        [HttpDelete("aprovador-management/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse(await _aprovadorService.Remove(id));
        }     
    }
}
