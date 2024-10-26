using System;

namespace UescColcicAPI.Services.InputModels;

public class UserInputModel
{
    public required string Username { get; set; }
    public required string  Password { get; set; }
    public required string Roles { get; set; }

}
