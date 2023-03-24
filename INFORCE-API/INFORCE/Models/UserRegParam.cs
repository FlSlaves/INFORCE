using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace INFORCE.Models
{
    public class UserRegParam
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Role Roles { get; set; }
    }
    public enum Role
    {
        User,
        Admin
    }
}
