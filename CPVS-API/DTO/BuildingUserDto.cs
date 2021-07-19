using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPVS_API.DTO
{
    public class BuildingUserDto
    {
        public BuildingUserDto()
        {
            CreatedDate = DateTime.Now;
        }
        public int ID { get; set; }
        public int UserID { get; set; }
        public int BuildingID { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
