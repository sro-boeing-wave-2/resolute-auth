﻿using auth.Models;
using auth.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace auth.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    class AuthController: Controller
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
                string token = _authService.Login(credentials.Email, credentials.Password);
                return Ok(token);
            } catch (Exception e)
            {
                return NotFound();
            }
        }

        [Route("user/add")]
        [HttpPost]
        public IActionResult AddUserCredentials([FromBody] string email, [FromBody] string password)
        {
            Boolean result = _authService.AddUserCreadentials(email, password);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Route("token/verify")]
        [HttpPost]
        public IActionResult VerifyUserToken([FromBody] string token)
        {
            UserHeaders result = _authService.VerifyUserToken(token);
            if (result != null)
            {
                return Ok(JsonConvert.SerializeObject(result));
            } else
            {
                return NotFound();
            }
        }

    }
}
