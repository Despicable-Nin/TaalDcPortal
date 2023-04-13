// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication2.Areas.Identity.Pages.Account;

public class LogoutModel : PageModel
{
    private readonly ILogger<LogoutModel> _logger;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;

    public LogoutModel(SignInManager<IdentityUser> signInManager, ILogger<LogoutModel> logger, IConfiguration configuration)
    {
        _signInManager = signInManager;
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<IActionResult> OnPost(string returnUrl = null)
    {
        await _signInManager.SignOutAsync();
        _logger.LogInformation("User logged out.");

        return LocalRedirect(_configuration["SubURL"] + "/Identity/Account/Login");
    }
}