using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Services.Interfaces;

namespace App.Controllers.Admin
{
    [Route("api/admin/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _accountService.GetUsersAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            return Ok(await _accountService.GetUserByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoles()
        {
            await _accountService.CreateRolesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _accountService.GetRolesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById([FromRoute] string id)
        {
            return Ok(await _accountService.GetRoleByIdAsync(id));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromQuery] string id)
        {
            await _accountService.DeleteUserAsync(id);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddRoleToUser([FromBody] UserRoleDto request)
        {
            var response = await _accountService.AddRoleToUserAsync(request);
            if (!response.IsSuccess)
               return BadRequest(response);

            return CreatedAtAction(nameof(AddRoleToUser),response);
        }

        //[HttpDelete]
        //public async Task<IActionResult> DeleteRoleFromUser([FromQuery] UserRoleDto request)
        //{
        //    var response = await _accountService.DeleteRoleFromUserAsync
        //}
    }
}