using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPVS_API.DTO
{
    public class TimeLineDto
    {
        public int TimeLine_ID { get; set; }
        public string TimeLine_Action { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Role_Name { get; set; }
        public int ToDoList_ID { get; set; }
        public DateTime? Signed_Time { get; set; }
        public DateTime? Approve_Time { get; set; }

    }
}
