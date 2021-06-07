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
    public class EscritorioController : ApiController
    {
        private readonly IEscritorioService _escritorioService;
        public EscritorioController(IEscritorioService escritorioService)
        {
            _escritorioService = escritorioService;
        }
        [AllowAnonymous]
        [HttpGet("escritorio-management")]
        public async Task<IEnumerable<EscritorioViewModel>> Get()
        {
            return await _escritorioService.GetAll();
        }
        [AllowAnonymous]
        [HttpGet("escritorio-management/{id}")]
        public async Task<EscritorioViewModel> Get(Guid id)
        {
            return await _escritorioService.GetById(id);
        }
        [AllowAnonymous]
        [HttpPost("escritorio-management")]
        public async Task<IActionResult> Post([FromBody] EscritorioViewModel escritorioViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _escritorioService.Register(escritorioViewModel));
        }
        [AllowAnonymous]
        [HttpPut("escritorio-management")]
        public async Task<IActionResult> Put([FromBody] EscritorioViewModel escritorioViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _escritorioService.Update(escritorioViewModel));
        }
        [AllowAnonymous]
        [HttpDelete("escritorio-management/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse(await _escritorioService.Remove(id));
        }
    }
}
