using auth_api.Dtos;
using auth_api.Dtos.Criterias;
using auth_api.Dtos.Payloads;
using AuthApi.Dtos;
using AuthApi.Models;
using AuthApi.Repository;
using AuthApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : BaseController
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger, IUserRepository userRepository) : base(userRepository)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetUser()
    {
        try
        {
            var user = ReadToken();
            return Ok(new UserResponse
            {
                Name = user.Name,
                Email = user.Email,
                Guid = user.Guid
            });
        }
        catch (Exception ex)
        {
            _logger.LogError("Ocorreu um erro no login: " + ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorDto()
            {
                Description = "Ocorreu o seguinte erro: " + ex.Message,
                Status = StatusCodes.Status500InternalServerError
            });
        }
    }

    [HttpPut]
    public IActionResult UpdateUser([FromBody] UserPayload payload)
    {
        try
        {
            if (payload == null || !_userRepository.VerifyUserById(payload.Id.Value))
            {
                return BadRequest(new ResponseErrorDto()
                {
                    Description = "Usuário informado não existe",
                    Status = StatusCodes.Status400BadRequest,
                });
            }
            if (payload != null)
            {
                var errors = new List<string>();
                if (String.IsNullOrEmpty(payload.Name) || String.IsNullOrWhiteSpace(payload.Name))
                {
                    errors.Add("Nome inválido");
                }
                if (errors.Count > 0)
                {
                    return BadRequest(new ResponseErrorDto()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        ErrorMsgs = errors
                    });
                }
            }

            var userModel = new UserModel()
            {
                Id = payload.Id.Value,
                Name = payload.Name,
                Email = payload.Email.ToLower(),
                UserType = payload.UserType,
                CPF = payload.CPF,
                CNPJ = payload.CNPJ,
                CorporateName = payload.CorporateName,
                FantasyName = payload.FantasyName,
                Password = MD5Utils.GenerateHashMD5(payload.Password),
                Guid = Guid.NewGuid().ToString()
            };

            _userRepository.Update(userModel);
            return Ok(new ResponseSuccessDto()
            {
                Description = "Usuário atualizado com sucesso!",
                Status = StatusCodes.Status200OK
            });
        }
        catch (Exception ex)
        {
            _logger.LogError("Ocorreu um erro ao salvar o usuário: " + ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorDto()
            {
                Description = "Ocorreu o seguinte erro: " + ex.Message,
                Status = StatusCodes.Status500InternalServerError
            });
        }
    }


    [HttpPost]
    [AllowAnonymous]
    public IActionResult SaveUser([FromBody] UserPayload payload)
    {
        try
        {
            if (payload != null)
            {
                var errors = new List<string>();
                if (String.IsNullOrEmpty(payload.Name) || String.IsNullOrWhiteSpace(payload.Name))
                {
                    errors.Add("Nome inválido");
                }
                if (String.IsNullOrEmpty(payload.Email) || String.IsNullOrWhiteSpace(payload.Email) || !payload.Email.Contains("@"))
                {
                    errors.Add("Email inválido");
                }
                if (String.IsNullOrEmpty(payload.Password) || String.IsNullOrWhiteSpace(payload.Password))
                {
                    errors.Add("Senha inválida");
                }
                if (errors.Count > 0)
                {
                    return BadRequest(new ResponseErrorDto()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        ErrorMsgs = errors
                    });
                }

                var userModel = new UserModel()
                {
                    Name = payload.Name,
                    Email = payload.Email.ToLower(),
                    UserType = payload.UserType,
                    CPF = payload.CPF,
                    CNPJ = payload.CNPJ,
                    CorporateName = payload.CorporateName,
                    FantasyName = payload.FantasyName,
                    Password = MD5Utils.GenerateHashMD5(payload.Password),
                    Guid = Guid.NewGuid().ToString()
                };

                if (!_userRepository.VerifyEmail(payload.Email))
                {
                    _userRepository.Save(userModel);
                }
                else
                {
                    return BadRequest(new ResponseErrorDto()
                    {
                        Description = "Usuário informado já existe",
                        Status = StatusCodes.Status400BadRequest,
                    });
                }
            }

            return Ok(new ResponseSuccessDto()
            {
                Description = "Usuário criado com sucesso!",
                Status = StatusCodes.Status200OK
            });
        }
        catch (Exception ex)
        {
            _logger.LogError("Ocorreu um erro ao salvar o usuário: " + ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorDto()
            {
                Description = "Ocorreu o seguinte erro: " + ex.Message,
                Status = StatusCodes.Status500InternalServerError
            });
        }
    }
}