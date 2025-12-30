using DevTrivia.API.Capabilities.Category.Models;
using DevTrivia.API.Capabilities.Trivia.Models;
using DevTrivia.API.Capabilities.User.Database.Entities;
using DevTrivia.API.Capabilities.User.Models;
using DevTrivia.API.Capabilities.User.Services.Interfaces;
using DevTrivia.API.Infrastructure.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DevTrivia.API.Capabilities.User.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<CategoryController> _logger;

    public CategoryController(IUserService userService, ILogger<CategoryController> logger)
    {
        _userService = userService;
        _logger = logger;
    }
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ApiResponse<CategoryResponse>>> Create([FromBody] CategoryRequest request, CancellationToken cancellationToken)
    {
        //if ()
        //{
            
        //}
        return Ok();
    }
    [HttpPut]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update([FromBody] CategoryRequest request, CancellationToken cancellationToken)
    {

        return Ok();
    }
    [HttpDelete]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Delete([FromBody] CategoryRequest request, CancellationToken cancellationToken)
    {
        return Ok();
    }
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Searches([FromBody] CategoryRequest request, CancellationToken cancellationToken)
    {
        //if ()
        //{

        //}
        return Ok();
    }
    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> SearchesId([FromBody] CategoryRequest request, CancellationToken cancellationToken)
    {
        //if ()
        //{

        //}
        return Ok();
    }
}
