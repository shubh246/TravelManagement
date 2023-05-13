using Microsoft.AspNetCore.Mvc;
using System.Net;
using TravelManagement.Models.Dto;
using TravelManagement.Models;
using TravelManagement.Repository;

namespace TravelManagement.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthRepository authRepository;
        protected ApiResponse response;
        public AuthController(IAuthRepository _authRepository)
        {
            authRepository = _authRepository;
            this.response = new();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var loginResponse = await authRepository.Login(model);
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.ErrorMessage.Add("Username or password is incorrect");
                return BadRequest(response);




            }
            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
            response.Result = loginResponse;
            return Ok(response);

        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model)
        {
            bool ifUserNameUnique = authRepository.IsUniqueUser(model.UserName);
            if (!ifUserNameUnique)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.ErrorMessage.Add("Username pr password is incorrect");
                return BadRequest(response);
            }
            var user = await authRepository.Register(model);
            if (user == null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.ErrorMessage.Add("Error while registering");
                return BadRequest(response);
            }
            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
            return Ok(response);
        }
    }
}
