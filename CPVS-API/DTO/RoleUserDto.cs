using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPVS_API.DTO
{
    public class RoleUserDto
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public bool? Status { get; set; }
        public DateTime Create_Time { get; set; }
    }
}
