using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mypos_api.Models;
using mypos_api.Services;

namespace mypos_api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        ILogger<AuthController> _logger;
        private readonly IAuthRepository authRepository;

        public AuthController(ILogger<AuthController> logger, IAuthRepository authRepository)
        {
            _logger = logger;
            this.authRepository = authRepository;
        }

        // localhost..../api/auth/login [POST]
        [HttpPost("login")]
        public IActionResult Login([FromBody] Users model)
        {
            try
            {
                (Users result, string token) = authRepository.Login(model);
                if(result == null){
                  return NotFound();
                }

                if(String.IsNullOrEmpty(token)){
                  return Ok("password invalid");
                }

                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("[action]")]
        public IActionResult Register([FromBody] Users model)
        {
            try
            {
                authRepository.Register(model);
                return Ok("register success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}