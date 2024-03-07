namespace Comment.Controllers
{
    using Category.Models;
    using Comment.Models;
    using CommonResponse.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Users.Data;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly UserContext _commentContext;
        private readonly IConfiguration _configuration;
        public CommentController(UserContext commentContext, IConfiguration configuration)
        {
            _commentContext = commentContext;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetComments(int? id, int? postId)
        {
            var response = new CommonResponse();
            IEnumerable<CommentModel> comments;

            if (id != null)
            {
                comments = _commentContext.Comment.Where(item => item.Id == id);
            }
            else if (postId != null)
            {
                comments = _commentContext.Comment.Where(item => item.PostId == postId).ToList();
            }
            else
            {
                comments = _commentContext.Comment.ToList();
            }

            response.statusCode = 200;
            response.message = "All comments related to posts";
            response.data = comments;

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentModel comment)
        {
            var response = new CommonResponse();
            try
            {
                var existingPost = _commentContext.Posts.FirstOrDefault(post => post.Id == comment.PostId);

                if (existingPost == null)
                {
                    response.statusCode = 404;
                    response.message = "There is no posts to add comments";

                    return NotFound(response);
                }

                await _commentContext.Comment.AddAsync(comment);
                await _commentContext.SaveChangesAsync();

                response.statusCode = 200;
                response.message = "Comment Added Successfully";

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