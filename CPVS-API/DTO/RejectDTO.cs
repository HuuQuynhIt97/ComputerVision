using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPVS_API.DTO
{
    public class RejectDTO
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int UserSenderID { get; set; }
        public string Remark { get; set; }
    }
}
