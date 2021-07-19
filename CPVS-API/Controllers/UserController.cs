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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPut("{userId}/{password}")]
        public async Task<IActionResult> ChangePassword(int userId, string password)
        {
            return Ok(await _userService.ChangePassword(userId, password));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var user = await _userService.GetAllAsync();
            return Ok(user);
        }

        [HttpGet("{id}")]
        public IActionResult Single(int id)
        {
            var userDetails = _userService.GetById(id);
            return Ok(userDetails);
        }
        [HttpGet("{text}")]
        public async Task<IActionResult> Search([FromQuery]PaginationParams param, string text)
        {
            var lists = await _userService.Search(param, text);
            Response.AddPagination(lists.CurrentPage, lists.PageSize, lists.TotalCount, lists.TotalPages);
            return Ok(lists);
        }
       
        [HttpPost]
        public async Task<IActionResult> Create(UserDto create)
        {
            return Ok(await _userService.AddUser(create));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserDto update)
        {
            if (await _userService.Update(update))
                return NoContent();
            return BadRequest($"Updating user detail {update.ID} failed on save");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _userService.Delete(id))
                return NoContent();
            throw new Exception("Error deleting the user detail");
        }
    }
}