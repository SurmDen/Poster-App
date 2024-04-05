using Microsoft.AspNetCore.Mvc;
using Poster.Core.Models;
using Poster.Data.Interfaces;

namespace Poster.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : Controller
    {
        private ICategoryRepository categoryRepository;
        private ILogger<CategoryController> logger;

        public CategoryController(ICategoryRepository categoryRepository, ILogger<CategoryController> logger)
        {
            this.categoryRepository = categoryRepository;
            this.logger = logger;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            try
            {
                IEnumerable<PostCategory> categories = await categoryRepository.GetPostCategoriesAsync();

                logger.LogInformation("Get all categories requests");

                return Ok(categories);
            }
            catch (Exception e)
            {
                logger.LogError("{@Exception} uccured while trying to get all categories", e);

                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryAsync(long id)
        {
            try
            {
                PostCategory category = await categoryRepository.GetCategoryByIdAsync(id);

                logger.LogInformation("Get category with id: {@ID} requests", id);

                return Ok(category);
            }
            catch (Exception e)
            {
                logger.LogError("{@Exception} uccured while trying to get category with {@ID}", e, id);

                return BadRequest();
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] PostCategory category)
        {
            try
            {
                await categoryRepository.CreateCategoryAsync(category);

                logger.LogInformation("Create category requests");

                return Ok();
            }
            catch (Exception e)
            {
                logger.LogError("{@Exception} uccured while trying to create category  {@Category}", e, category);

                return BadRequest();
            }
        }
    }
}
