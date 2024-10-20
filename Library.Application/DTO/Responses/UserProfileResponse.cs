﻿namespace Library.Application.DTO.Responses
{
    public class UserProfileResponse
    {
        public required Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Phone { get; set; }
    }
}
