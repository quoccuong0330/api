using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WebAPI.DTOs.Account;
using WebAPI.Interfaces;
using WebAPI.Models;
using WebAPI.Service;

namespace WebAPI.Controllers;


[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase {
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;
    public AccountController(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager) {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterAccountDTO accountDto) {
        try {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new User{
                UserName = accountDto.Username,
                Email = accountDto.Email
            };
            var createUser = await _userManager.CreateAsync(user, accountDto.Password);
            if (createUser.Succeeded) {
                var roleResult = await _userManager.AddToRoleAsync(user, "User");
                if (roleResult.Succeeded) {
                    return Ok(new NewUserDTO {
                        UserName = user.UserName,
                        Email = user.Email,
                        Token = _tokenService.CreateToken(user)
                    });
                } else {
                    return StatusCode(500, roleResult.Errors);
                }
            }
            else {
                return StatusCode(500, createUser.Errors);

            }
        }
        catch (Exception e) {
            return StatusCode(500, e);

        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginAccountDTO accountDto) {
        if (!ModelState.IsValid) return BadRequest();
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == accountDto.Username.ToLower());
        if (user == null) return Unauthorized("Invalid username");
        var result = await _signInManager.CheckPasswordSignInAsync(user, accountDto.Password, false);
        if (!result.Succeeded) return Unauthorized("Username or password is incorrect");
        return Ok(new NewUserDTO {
            UserName = user.UserName,
            Email = user.Email,
            Token = _tokenService.CreateToken(user)
        });
    }
}