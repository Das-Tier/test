using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeStyle.Models
{
    public class Credential
    {
        public int ID { get; set; }
        public int AdminID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }

        public Credential() { }
        public Credential(int id, int adminid, string login, string password, int roleid)
        {
            ID = id;
            AdminID = adminid;
            Login = login;
            Password = password;
            RoleID = roleid;
        }
    }
}
