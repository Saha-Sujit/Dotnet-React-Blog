namespace Post.Controllers
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using CommonResponse.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Post.Models;
    using Users.Data;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly UserContext _postContext;
        private readonly IConfiguration _configuration;
        public PostController(UserContext postContext, IConfiguration configuration)
        {
            _postContext = postContext;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetPosts(int? id, int? userId)
        {
            var response = new CommonResponse();
            IEnumerable<PostModel> posts;
            if (userId != null)
            {
                posts = _postContext.Posts.Where(item => item.UserId == userId).ToList();
            }
            else if (id != null)
            {
                posts = _postContext.Posts.Where(item => item.Id == id);
            }
            else
            {
                posts = _postContext.Posts.ToList();
            }

            response.statusCode = 200;
            response.message = "Your all posts is succesfully fetched";
            response.data = posts;
            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPost(PostModel post)
        {
            var response = new CommonResponse();

            await _postContext.Posts.AddAsync(post);
            await _postContext.SaveChangesAsync();

            response.statusCode = 200;
            response.message = "Your Post is Added";

            return Ok(response);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdatePost(PostModel post)
        {
            var response = new CommonResponse();
            var existingPost = _postContext.Posts.Where(item => item.Id == post.Id);

            _postContext.Posts.Update(post);
            await _postContext.SaveChangesAsync();

            response.statusCode = 200;
            response.message = "You Post is updated";

            return Ok(response);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok();
        }
    }
}