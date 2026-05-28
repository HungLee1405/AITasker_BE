using AITasker.API.Filters;
using AITasker.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AITasker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UsersController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("test-connection")]
    public async Task<IActionResult> TestConnection()
    {
        try
        {
            // Thử query 1 bản ghi để check DB
            var usersCount = await _context.Users.CountAsync();
            return Ok(new { Message = "Kết nối Database thành công!", TotalUsers = usersCount });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Lỗi kết nối DB", Error = ex.Message });
        }
    }

    [HttpGet("test-expert-profile")]
    [Authorize]
    [ExpertProfileRequired]
    public IActionResult TestExpertProfile()
    {
        return Ok(new { Message = "Truy cập thành công! Bạn là Client hoặc Expert đã hoàn thành hồ sơ." });
    }
}
