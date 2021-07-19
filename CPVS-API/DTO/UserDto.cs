using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPVS_API.DTO
{
    public class UserDto
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmployeeID { get; set; }
        public int LevelOC { get; set; }
        public int OCID { get; set; }
        public int RoleID { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime ModifyTime { get; set; }
       
    }
}
