namespace User.Controllers
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using BCrypt.Net;
    using CommonResponse.Models;
    using GetUsersDTO.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using UserLoginDTO.Models;
    using Users.Data;
    using Users.Models;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _userContext;
        private readonly IConfiguration _configuration;
        public UserController(UserContext userContext, IConfiguration configuration)
        {
            _userContext = userContext;
            _configuration = configuration;
        }

        private string CreateToken(UserModel user)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name!),
                new Claim(ClaimTypes.Email, user.Email!)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                // issuer: _configuration["JWT:ValidAudience"],
                // audience: _configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin user)
        {
            var existingUser = await _userContext.Users.FirstOrDefaultAsync(item => item.Email == user.Email);
            var userResponse = new UserLoginDTO();
            var result = new CommonResponse();
            if (existingUser != null)
            {
                if (BCrypt.Verify(user.password, existingUser.password))
                {
                    userResponse.Name = existingUser.Name;
                    userResponse.Id = existingUser.Id;
                    userResponse.Email = existingUser.Email;
                    userResponse.Token = CreateToken(existingUser);

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
            var newUserModel = new UserModel();
            var result = new CommonResponse();
            if (existingUser != null)
            {
                result.statusCode = 400;
                result.message = "User is already have an account";

                return BadRequest(result);
            }

            string passwordHash = BCrypt.HashPassword(user.password);
            newUserModel.Id = user.Id;
            newUserModel.Name = user.Name;
            newUserModel.Email = user.Email;
            newUserModel.password = passwordHash;

            await _userContext.Users.AddAsync(newUserModel);
            await _userContext.SaveChangesAsync();

            userResponse.Name = user.Name;
            userResponse.Id = user.Id;
            userResponse.Email = user.Email;

            result.statusCode = 200;
            result.message = "Registered Successfully";
            result.data = userResponse;

            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var response = new CommonResponse();
            var getUsers = _userContext.Users.ToList();
            var getUsersWithoutPassword = getUsers.Select(user => new GetUsersDTO
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
            });
            response.data = getUsersWithoutPassword;
            response.statusCode = 200;
            response.message = "Here is the all users";
            return Ok(response);
        }
    }
}