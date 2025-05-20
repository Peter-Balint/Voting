using AutoMapper;
using Voting.DataAccess.Models;
using Voting.DataAccess.Services;
using Voting.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Voting.WebAPI.Controllers;

/// <summary>
/// Felhasználók vezérlője
/// </summary>
[ApiController]
[Route("api/users/")]
public class UsersController : ControllerBase
{
    private readonly UsersService _userService;
    private readonly IMapper _mapper;

    public UsersController(UsersService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(statusCode: StatusCodes.Status201Created, type: typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
    {
        try
        {

            var user = new User() {UserName = userDto.Email,  Name = userDto.Name, Email = userDto.Email };

            await _userService.RegistrateAsync(user, userDto.Password!);

            var userResponseDto = _mapper.Map<UserDto>(user);

            return StatusCode(StatusCodes.Status201Created, userResponseDto);
        }
        catch (InvalidDataException e)
        {
            return Conflict(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    /// <summary>
    /// Bejelentkezés
    /// </summary>
    /// <param name="loginDto"></param>
    /// <response code="200">Sikeres bejelentkezés</response>
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var loggedInUser = await _userService.LoginAsync(loginDto.Email, loginDto.Password);
            var userResponseDto = _mapper.Map<UserDto>(loggedInUser);

            return Ok(userResponseDto);
        }
        catch (AccessViolationException e)
        {
            return StatusCode(StatusCodes.Status403Forbidden, e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    /// <summary>
    /// Bejelentkezés
    /// </summary>
    /// <param name="loginDto"></param>
    /// <response code="200">Sikeres bejelentkezés</response>
    [HttpPost]
    [Route("login-token")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> LoginToken([FromBody] LoginDto loginDto)
    {
        try
        {
            var loggedInUser = await _userService.LoginAsync(loginDto.Email, loginDto.Password);
            var userResponseDto = _mapper.Map<UserDto>(loggedInUser);

            // access token küldése header-ben
            Response.Headers.Append("X-Access-Token", $"{loggedInUser.Email}|{loggedInUser.AccessToken}");
            Response.Headers.Append("Access-Control-Expose-Headers", "X-Access-Token"); // CORS

            return Ok(userResponseDto);
        }
        catch (AccessViolationException e)
        {
            return StatusCode(StatusCodes.Status403Forbidden, e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    /// <summary>
    /// Kijelentkezés
    /// </summary>
    /// <response code="200">Sikeres kijelentkezés</response>
    [HttpPost]
    [Route("logout")]
    [Authorize]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent, type: typeof(void))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Logout()
    {
        await _userService.LogoutAsync();

        return NoContent();
    }
}
