namespace Post.Controllers
{
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
        public async Task<IActionResult> GetPosts()
        {
            var response = new CommonResponse();
            var posts = _postContext.Posts.ToList();
            response.statusCode = 200;
            response.message = "All Posts are here";
            response.data = posts;

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> GetPostById(int id)
        {
            var response = new CommonResponse();
            var postById = _postContext.Posts.FirstOrDefault(item => item.Id == id);
            response.statusCode = 200;
            response.message = "Post Releted to id";
            response.data = postById;
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> GetPostsByUserId(int userId)
        {
            var response = new CommonResponse();
            var postById = _postContext.Posts.ToList().FindAll(item => item.UserId == userId);
            response.statusCode = 200;
            response.message = "Post Releted to UserId";
            response.data = postById;
            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPost(PostModel post)
        {
            await _postContext.Posts.AddAsync(post);
            await _postContext.SaveChangesAsync();
            return Ok();
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdatePost()
        {
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok();
        }
    }
}