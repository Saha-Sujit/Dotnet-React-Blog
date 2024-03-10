namespace Media.Controllers
{
    using System.IdentityModel.Tokens.Jwt;
    using CommonResponse.Models;
    using Media.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Users.Data;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly UserContext _mediaContext;
        public MediaController(IWebHostEnvironment environment, UserContext mediaContext)
        {
            _environment = environment;
            _mediaContext = mediaContext;
        }

        private int GetUserIdFromClaims()
        {
            string authToken = Request.Headers.Authorization!;
            var jwtToken = authToken?.Split(" ")[1];

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(jwtToken);
            var userId = Int32.Parse(token.Claims.FirstOrDefault()!.Value);
            return userId;
        }

        private async Task<string> SaveFileAsync(IFormFile file, string fileName)
        {
            string folderPath = Path.Combine(_environment.ContentRootPath, "Images");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = Path.Combine(folderPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"Images/{fileName}";
        }

        [HttpGet]
        public IActionResult GetAllImages()
        {
            return Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddImage()
        {
            var response = new CommonResponse();
            var newMedia = new MediaModel();
            try
            {
                newMedia.UserId = GetUserIdFromClaims();

                var file = Request.Form.Files[0];
                string fileName = file.FileName;

                string urlPath = await SaveFileAsync(file, fileName);

                newMedia.ImagePath = urlPath;

                await _mediaContext.Media.AddAsync(newMedia);
                await _mediaContext.SaveChangesAsync();

                response.statusCode = 200;
                response.message = "Image is uploaded and inserted successfully";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.statusCode = 500;
                response.message = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteImage(int id)
        {
            return Ok();
        }
    }
}