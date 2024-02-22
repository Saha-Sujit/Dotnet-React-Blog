namespace User.Controllers
{
    using CommonResponse.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using UserLoginDTO.Models;
    using Users.Data;
    using Users.Models;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _userContext;
        public UserController(UserContext userContext)
        {
            _userContext = userContext;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin user)
        {
            var existingUser = await _userContext.Users.FirstOrDefaultAsync(item => item.Email == user.Email);
            var userResponse = new UserLoginDTO();
            var result = new CommonResponse();
            if (existingUser != null)
            {
                if (existingUser.password == user.password)
                {
                    userResponse.Name = existingUser.Name;
                    userResponse.Id = existingUser.Id;
                    userResponse.Email = existingUser.Email;

                    result.statusCode = 200;
                    result.message = "User is successfully logged in";
                    result.data = userResponse;

                    return Ok(result);
                }

                result.statusCode = 400;
                result.message = "Email or password is wrong";

                return BadRequest(result);
            }

            result.statusCode = 404;
            result.message = "User is not registered";

            return NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserModel user)
        {
            var existingUser = await _userContext.Users.FirstOrDefaultAsync(item => item.Email == user.Email);
            var userResponse = new UserLoginDTO();
            var result = new CommonResponse();
            if (existingUser != null)
            {
                result.statusCode = 400;
                result.message = "User is already have an account";

                return BadRequest(result);
            }
            await _userContext.Users.AddAsync(user);
            await _userContext.SaveChangesAsync();

            userResponse.Name = user.Name;
            userResponse.Id = user.Id;
            userResponse.Email = user.Email;

            result.statusCode = 200;
            result.message = "Registered Successfully";
            result.data = userResponse;

            return Ok(result);
        }
    }
}