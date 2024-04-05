using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poster.Core.Models;
using Poster.Data.Interfaces;

namespace Poster.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostController : Controller
    {
        private IPostRepository postRepository;
        private ILogger logger;

        public PostController(IPostRepository postRepository, ILogger<PostController> logger)
        {
            this.postRepository = postRepository;
            this.logger = logger;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllPostsAsync()
        {
            try
            {
                IEnumerable<Post> posts = await postRepository.GetAllPostsAsync();

                logger.LogInformation("Get all posts request");

                return Ok(posts);
            }
            catch (Exception e)
            {
                logger.LogError("{@Exception} uccured while trying to get all posts", e);

                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostAsync(long id)
        {
            try
            {
                Post post = await postRepository.GetPostByIdAsync(id);

                logger.LogInformation("Get post with id: {@ID} request", id);

                return Ok(post);
            }
            catch (Exception e)
            {
                logger.LogError("{@Exception} uccured while trying to get post with id : {@ID}", e, id);

                return BadRequest();
            }
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreatePostAsync([FromBody] CreatePostModel postModel)
        {
            try
            {
                Post post = new Post()
                {
                    Title = postModel.Title,
                    Introdution = postModel.Introdution,
                    MainPart = postModel.MainPart,
                    Conclusion = postModel.Conclusion,
                    UserId = postModel.UserId,
                    PostCategoryId = postModel.PostCategoryId
                };

                await postRepository.CreatePostAsync(post);

                logger.LogInformation("Create post request");

                return Ok();
            }
            catch (Exception e)
            {
                logger.LogError("{@Exception} uccured while trying to " +
                    "create post with user id : {@user}, category id: {@cat}", e, postModel.UserId, postModel.PostCategoryId);

                return BadRequest();
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeletePostAsync([FromBody] long id)
        {
            try
            {
                await postRepository.DeletePostAsync(id);

                logger.LogInformation("Delete post with id: {@ID} request", id);

                return Ok();
            }
            catch (Exception e)
            {
                logger.LogError("{@Exception} uccured while trying to delete post with {@ID}", e, id); ;

                return BadRequest();
            }
        }
    }
}
