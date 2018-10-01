using auth.Models;
using auth.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace auth.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {

        private IAuthService _authService;

        public AuthController(IAuthService _authService)
        {
            this._authService = _authService;
        }
        

        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] UserCredentialsDto credentials)
        {
            try
            {
                string token = _authService.Login(credentials.Username, credentials.Password).Result;
                return Ok(token);
            } catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.GetBaseException());
                return Unauthorized();
            }
        }

        [Route("user/add")]
        [HttpPost]
        public IActionResult AddUserCredentials([FromBody] UserCredentialsDto userCredentials)
        {
            Boolean result = _authService.AddUserCreadentials(userCredentials.Username, userCredentials.Password);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Route("token/verify")]
        [HttpGet]
        public IActionResult VerifyUserToken([FromQuery] string token)
        {
            UserHeaders result = _authService.VerifyUserToken(token);
            if (result != null)
            {
                return Ok(result);
            } else
            {
                return NotFound();
            }
        }

    }
}
