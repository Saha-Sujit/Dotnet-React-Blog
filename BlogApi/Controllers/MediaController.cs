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

            var newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + fileName;

            string filePath = Path.Combine(folderPath, newFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"Images/{newFileName}";
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllImages()
        {
            var response = new CommonResponse();
            try
            {
                var userId = GetUserIdFromClaims();
                var images = _mediaContext.Media.Where(item => item.UserId == userId).ToList();

                response.statusCode = 200;
                response.message = "All images fetched";
                response.data = images;

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.statusCode = 500;
                response.message = ex.Message;
                return StatusCode(500, response);
            }
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

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var response = new CommonResponse();
            var userId = GetUserIdFromClaims();
            var rootFolder = "Images/";
            var existingFile = _mediaContext.Media.FirstOrDefault(item => item.Id == id);
            var fileName = existingFile!.ImagePath!.Split("/")[1].ToString();

            try
            {
                if (existingFile.UserId != userId)
                {
                    response.statusCode = 400;
                    response.message = "Sorry You are not the owner of this image";

                    return BadRequest(response);
                }

                if (System.IO.File.Exists(Path.Combine(rootFolder, fileName)))
                {
                    System.IO.File.Delete(Path.Combine(rootFolder, fileName));

                    _mediaContext.Media.Remove(existingFile);
                    await _mediaContext.SaveChangesAsync();

                    response.statusCode = 200;
                    response.message = "File and data deleted successfully";
                }
                else
                {
                    response.statusCode = 404;
                    response.message = "File does not exist";
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.statusCode = 500;
                response.message = ex.Message;
                return StatusCode(500, response);
            }
        }
    }
}