﻿namespace Library.Application.DTO.Requests
{
    public class SignInRequest
    {
        public required string Login { get; set; }
        public required string Password { get; set; }
    }
}
