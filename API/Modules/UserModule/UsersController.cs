using AITasker_Modular.Modules.UserModule.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AITasker_Modular.Modules.UserModule;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _userService.RegisterAsync(dto.Email, dto.Password, dto.FullName, dto.Role);
        return result.Contains("already exists", StringComparison.OrdinalIgnoreCase)
            ? BadRequest(new { message = result })
            : Ok(new { message = result });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _userService.LoginAsync(dto.Email, dto.Password);
        return result.Contains("Invalid", StringComparison.OrdinalIgnoreCase)
            ? BadRequest(new { message = result })
            : Ok(new { message = result });
    }
}
