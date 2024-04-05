using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poster.Core.Models;
using Poster.Data.Interfaces;
using Poster.Identity.Interfaces;
using Poster.Identity.Models;

namespace Poster.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private IUserRepository userRepository;
        private ITokenService tokenService;
        private ILogger<UserController> logger;

        public UserController(IUserRepository userRepository, ITokenService tokenService, ILogger<UserController> logger)
        {
            this.userRepository = userRepository;
            this.tokenService = tokenService;
            this.logger = logger;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUserAsync([FromBody]CreateUserModel userModel)
        {
            try
            {
                User user = new User()
                {
                    FullName = userModel.FullName,
                    Password = userModel.Password,
                    Email = userModel.Email
                };

                await userRepository.CreateUserAsync(user);

                TokenModel tokenModel = new TokenModel()
                {
                    UserEmail = user.Email,
                    UserName = user.FullName
                };

                string token = tokenService.GetToken(tokenModel);

                UserWithToken userWithToken = new UserWithToken()
                {
                    UserId = user.Id,
                    Token = token
                };

                logger.LogInformation("User account with email: {@email} and name: {@name} created", user.Email, user.FullName);

                return Ok(userWithToken);
            }
            catch (Exception e)
            {
                logger.LogError("{@Exception} occured while user creating. Request body: {@UserModel}", e, userModel);

                return BadRequest();
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUserAsync([FromBody]long id)
        {
            try
            {
                await userRepository.DeleteUserAsync(id);

                logger.LogInformation("User account with id: {@ID} deleted", id);

                return Ok();
            }
            catch (Exception e)
            {
                logger.LogError("{@Exception} occured while user deleting. Request body: {@ID}", e, id);

                return BadRequest();
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] User user)
        {
            try
            {
                await userRepository.UpdateUserAsync(user);

                logger.LogInformation("User account with email: {@email} and name: {@name} updated", user.Email, user.FullName);

                return Ok();
            }
            catch (Exception e)
            {
                logger.LogError("{@Exception} occured while user updating", e);

                return BadRequest();
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            try
            {
                IEnumerable<User> users = await userRepository.GetAllUsersAsync();

                logger.LogInformation("Get all users request");

                return Ok(users);
            }
            catch (Exception e)
            {
                logger.LogError("{@Exception} occured while getting all users", e);

                return BadRequest();
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync(long id)
        {
            try
            {
                User user = await userRepository.GetUserByIdAsync(id);

                logger.LogInformation("Get user with id: {@id} request", id);

                return Ok(user);
            }
            catch (Exception e)
            {
                logger.LogError("{@Exception} occured while getting user with id: {@ID}", e, id);

                return BadRequest();
            }
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignInAsync([FromBody]LoginModel loginModel)
        {
            try
            {
                User current = await userRepository.GetUserByNameAndPasswordAsync(loginModel);

                TokenModel tokenModel = new TokenModel()
                {
                    UserEmail = current.Email,
                    UserName = current.FullName
                };

                string token = tokenService.GetToken(tokenModel);

                current.Password = "";

                UserWithToken userWithToken = new UserWithToken()
                {
                    UserId = current.Id,
                    Token = token
                };

                logger.LogInformation("User with id {ID} sing in", current.Id);

                return Ok(userWithToken);
            }
            catch (Exception e)
            {
                logger.LogError("{@Exception} occured while user with login name: {@Name} try to sign in", e, loginModel.UserName);

                return BadRequest();
            }
        }

    }
}
