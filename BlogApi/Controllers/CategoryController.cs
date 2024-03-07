namespace Category.Controllers
{
    using System.IdentityModel.Tokens.Jwt;
    using Category.Models;
    using CommonResponse.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Users.Data;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly UserContext _categoryContext;
        private readonly IConfiguration _configuration;
        public CategoryController(UserContext categoryContext, IConfiguration configuration)
        {
            _categoryContext = categoryContext;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var response = new CommonResponse();
            try
            {
                var categories = _categoryContext.Category.ToList();
                if (categories == null)
                {
                    response.statusCode = 404;
                    response.message = "No Category found";

                    return NotFound(response);
                }

                response.statusCode = 200;
                response.message = "Your all categories are here";
                response.data = categories;

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
        public async Task<IActionResult> AddCategory(CategoryModel category)
        {
            var response = new CommonResponse();
            try
            {
                var existingCategogry = await _categoryContext.Category.FirstOrDefaultAsync(cat => cat.Name == category.Name!);
                if (existingCategogry != null)
                {
                    response.statusCode = 400;
                    response.message = "Your Category is already exists";

                    return BadRequest(response);
                }
                await _categoryContext.Category.AddAsync(category);
                await _categoryContext.SaveChangesAsync();

                response.statusCode = 200;
                response.message = "Your Category is Added";

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
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(CategoryModel category)
        {
            var response = new CommonResponse();
            try
            {
                var existingCategogry = _categoryContext.Category.FirstOrDefault(item => item.Id == category.Id);

                if (existingCategogry == null)
                {
                    response.statusCode = 404;
                    response.message = "No category found of the id";
                    return NotFound(response);
                }

                existingCategogry.Name = category.Name;

                _categoryContext.Category.Update(existingCategogry);
                await _categoryContext.SaveChangesAsync();

                response.statusCode = 200;
                response.message = "You Category is updated";

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
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var response = new CommonResponse();
            try
            {
                var existingCategory = _categoryContext.Category.FirstOrDefault(item => item.Id == id);

                if (existingCategory == null)
                {
                    response.statusCode = 404;
                    response.message = "No category found of the id";
                    return NotFound(response);
                }

                _categoryContext.Category.Remove(existingCategory!);
                await _categoryContext.SaveChangesAsync();

                response.statusCode = 200;
                response.message = "Your Category is Deleted";

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