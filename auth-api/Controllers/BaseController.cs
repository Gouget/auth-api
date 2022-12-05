using AuthApi.Models;
using AuthApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthApi.Controllers;

[Authorize]
public class BaseController : ControllerBase
{
    protected readonly IUserRepository _userRepository;

    public BaseController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    //Só é visível para as classes que herdam a BaseController
    protected UserModel ReadToken()
    {
        var userId = User.Claims.Where(x => x.Type == ClaimTypes.Sid).Select(x => x.Value).FirstOrDefault();
        if (String.IsNullOrEmpty(userId)) return null;

        return _userRepository.GetUserById(int.Parse(userId));
    }
}