﻿namespace AuthApi.Dtos;

public class LoginDto
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string Token { get; set; }
    public string Password { get; set; }
}