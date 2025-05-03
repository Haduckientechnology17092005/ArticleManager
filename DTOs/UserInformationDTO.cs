using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DTOs
{
    public class UserInformationDTO
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string Password { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string NewPassword { get; set; } = string.Empty;
        public string Role { get; set; }
    }
}
