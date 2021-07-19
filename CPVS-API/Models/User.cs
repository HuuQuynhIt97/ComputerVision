using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CPVS_API.Models
{
    public class User
    {
        public User()
        {
            this.ModifyTime = DateTime.Now;
        }

        public int ID { get; set; }
        public string Username { get; set; }
        public string EmployeeID { get; set; }
        public int LevelOC { get; set; }
        public int OCID { get; set; }
        public int RoleID { get; set; }
        public string Email { get; set; }
        public DateTime ModifyTime { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}