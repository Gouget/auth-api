using AuthApi.Dtos;
using AuthApi.Dtos.Criterias;
using AuthApi.Dtos.Payloads;
using AuthApi.Models.Services;
using AuthApi.Repository;
using AuthApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly ILogger<LoginController> _logger;
    private readonly IUserRepository _userRepository;

    public LoginController(ILogger<LoginController> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginPayload loginPayload)
    {
        try
        {
            if (!String.IsNullOrEmpty(loginPayload.Email)
                && !String.IsNullOrEmpty(loginPayload.Password)
                && !String.IsNullOrWhiteSpace(loginPayload.Email)
                && !String.IsNullOrWhiteSpace(loginPayload.Password))
            {
                var user = _userRepository.GetUserByEmailAndPassword(loginPayload.Email.ToLower(), MD5Utils.GenerateHashMD5(loginPayload.Password));

                return Ok(new LoginCriteria()
                {
                    Email = user.Email,
                    Name = user.Name,
                    Token = TokenService.GenerateToken(user)
                });
            }
            else
            {
                return BadRequest(new ResponseErrorDto()
                {
                    Description = "Usuário não digitou corretamente os campos de login",
                    Status = StatusCodes.Status400BadRequest
                });
            }

            throw new ArgumentException("Erro ao preencher os dados");
        }
        catch (Exception ex)
        {
            _logger.LogError("Ocorreu um erro no login: " + ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorDto()
            {
                Description = "Ocorreu um erro ao fazer o login",
                Status = StatusCodes.Status500InternalServerError
            });
        }
    }
}