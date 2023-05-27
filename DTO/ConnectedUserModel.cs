using Sign_Up_Form.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sign_Up_Form.DTO
{
    public class ConnectedUserModel
    {

        public User userConnected { get; set; }
        public List<string> accessRights { get; set; }
    }
}
