using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LivreriaWeb.Models;
using LivreriaWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LivreriaWeb.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        [Route("V1/Account")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            var user = new User()
            {
                UserName = model.UserName,
                Password = model.Password,
            };

            var token = TokenServices.GenerarateToken(model);
            user.Password = string.Empty;
            return new
            {
                user = user,
                token = token,

            };
        }
    }
}