﻿namespace ChatApplication.Shared.V1.Models.User;
public class CreateUserModel 
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}
