using System;
using System.Collections.Generic;
using System.Text;

namespace Sign_Up_Form.DTO
{
    public class ChangePasswordDTO
    {
        public int UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class LoginUserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
