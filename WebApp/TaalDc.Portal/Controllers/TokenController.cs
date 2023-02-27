using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaalDc.Portal.Services;

namespace TaalDc.Portal.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        
        private readonly IAccountService _accountService;

        public TokenController(
          
            IAccountService accountService) 
        {
            _accountService = accountService;
        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetToken(string email = "admin@strator.com")
        {
            return Ok(await _accountService.GetToken(email));

        }
    }
}
