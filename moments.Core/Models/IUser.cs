using System;

namespace moments.Core.Models
{
    public interface IUser
    {
        Guid Id { get; set; }
        string Email { get; set; }
        string UserName { get; set; }
        string PasswordHash { get; set; }
        string PhoneNumber { get; set; }
    }
}