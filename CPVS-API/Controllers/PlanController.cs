using System;
using System.Security.Claims;
using System.Threading.Tasks;
using CPVS_API.Helpers;
using CPVS_API._Services.Interface;
using CPVS_API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CPVS_API.Models;

namespace CPVS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PlanController : ControllerBase
    {
        private readonly IPlanService _planService;
        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        [HttpGet("{planID}")]
        public async Task<IActionResult> Start(int planID)
        {
            return Ok(await _planService.Start(planID));
        }

        [HttpDelete("{planID}")]
        public async Task<IActionResult> Delete(int planID)
        {
            return Ok(await _planService.Delete(planID));
        }

        [HttpGet("{planID}")]
        public async Task<IActionResult> Stop(int planID)
        {
            return Ok(await _planService.Stop(planID));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var settings = await _planService.GetAllAsync();
            return Ok(settings);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlanCount()
        {
            var settings = await _planService.GetAllPlanCount();
            return Ok(settings);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PlanDto create)
        {
            var model = await _planService.Add(create);
            return Ok(model);
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    // if (await _kindService.Delete(id))
        //    //     return NoContent();
        //    throw new Exception("Error deleting the Kind");
        //}


    }
}