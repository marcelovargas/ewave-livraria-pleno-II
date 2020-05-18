using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LivreriaWeb.Models;
using LivreriaWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LivreriaWeb.Controllers.API
{
    [Route("api/")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Retorna o token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("V1/Account")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            var response = _userManager.FindByEmailAsync(model.UserName);

            if (response != null)
            {
                var usuario = (IdentityUser)response.Result;

                var token = TokenServices.GenerarateToken(usuario);

                return new
                {
                    user = usuario,
                    token = token,

                };
            }

            return null;
            
        }
    }
}