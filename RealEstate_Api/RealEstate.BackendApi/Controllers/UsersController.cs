using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.System.Users;
using RealEstate.ViewModels.System.Users;

namespace RealEstate.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultToken = await _userService.Authencate(request);
            if (string.IsNullOrEmpty(resultToken))
            {
                return BadRequest("Tên đăng nhập hoặc mật khẩu không chính xác.");
            }
            return Ok(new { token = resultToken });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Register(request);
            if (!result)
            {
                return BadRequest("Đăng ký không thành công.");
            }
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(Guid userId)
        {
            var users = await _userService.GetById(userId);
            if (users == null)
                return BadRequest("Không tìm thấy được tin đăng");
            return Ok(users);
        }
    }
}
