using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPVS_API.Models
{
    public class RoleUser
    {
        public RoleUser()
        {
            this.Create_Time = DateTime.Now;
        }
        public int ID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public bool? Status { get; set; }
        public DateTime Create_Time { get; set; }
    }
}