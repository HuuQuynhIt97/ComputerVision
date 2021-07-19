using System;
using System.Security.Claims;
using System.Threading.Tasks;
using CPVS_API.Helpers;
using CPVS_API._Services.Interface;
using CPVS_API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CPVS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var settings = await _carService.GetAllAsync();
            return Ok(settings);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // if (await _kindService.Delete(id))
            //     return NoContent();
            throw new Exception("Error deleting the Kind");
        }
    }
}