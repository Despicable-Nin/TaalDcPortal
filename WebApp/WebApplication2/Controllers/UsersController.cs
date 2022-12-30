using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Services;
using WebApplication2.ViewModels.Users;

namespace WebApplication2.Controllers
{
    
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IAccountService _accountService;

        public UsersController(ILogger<UsersController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        public async Task<IActionResult> Index()
        {
            var vm = await _accountService.GetListOfUsersWithRoles();
            return View(vm);
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
    }
}