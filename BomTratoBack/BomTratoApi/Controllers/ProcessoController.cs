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
    public class ProcessoController : ApiController
    {
        private readonly IProcessoService _ProcessoService;
        public ProcessoController(IProcessoService ProcessoService)
        {
            _ProcessoService = ProcessoService;
        }
        [AllowAnonymous]
        [HttpGet("processo-management")]
        public async Task<IEnumerable<ProcessoViewModel>> Get()
        {
            return await _ProcessoService.GetAll();
        }
        [AllowAnonymous]
        [HttpGet("processo-management/{id}")]
        public async Task<ProcessoViewModel> Get(Guid id)
        {
            return await _ProcessoService.GetById(id);
        }
        [AllowAnonymous]
        [HttpPost("processo-management")]
        public async Task<IActionResult>Post([FromBody] ProcessoViewModel ProcessoViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _ProcessoService.Register(ProcessoViewModel));
        }
        [AllowAnonymous]
        [HttpPut("processo-management")]
        public async Task<IActionResult>Put([FromBody] ProcessoViewModel ProcessoViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _ProcessoService.Update(ProcessoViewModel));
        }
        [AllowAnonymous]
        [HttpDelete("processo-management")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse(await _ProcessoService.Remove(id));
        } 
    }
}
