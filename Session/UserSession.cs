using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Session
{
    public class UserSession
    {
        private static UserSession _instance;
        public static UserSession Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserSession();
                }
                return _instance;
            }
        }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public void SetUserSession(Guid userId, string username, string role)
        {
            UserId = userId;
            Username = username;
            Role = role;
        }
        public void ClearUserSession()
        {
            UserId = Guid.Empty;
            Username = string.Empty;
            Role = string.Empty;
            _instance = null;
        }
    }
}
