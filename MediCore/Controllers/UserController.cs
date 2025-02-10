using Application.Data_TransferModels.ResponseModels;
using Application.Data_TransferModels.UserDTO;
using Application.Interface.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Doctor,Nurse,Receptionist,Patient")]
    public class UserController : ControllerBase
    {
        private readonly IUser _userService;
        public UserController(IUser userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("CreateUser")]
        [AllowAnonymous]
        public async Task<ActionResult> CreateUser([FromBody] RegisterUserDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.CreateUser(model);
            return Ok(result);
        }

        [HttpPost]
        [Route("LoginUser")]
        [AllowAnonymous]
        public async Task<ActionResult> LoginUser([FromBody] LoginUserDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.LoginUser(model);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllUser")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetAllUser()
        {
            var result = await _userService.GetAllUser();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetUserbyId/{id:int}")]
        public async Task<ActionResult> GetUserbyId(int id)
        {
            var result = await _userService.GetUserbyId(id);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateUser/{id:int}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] UserUpdateDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.UpdateUser(id, model);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DelteUser/{id:int}")]
        public async Task<ActionResult> DelteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
            return Ok(result);
        }
    }
}
