using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPVS_API.Models
{
    public class ToDoList
    {
        public ToDoList()
        {
            this.Created_Date = DateTime.Now;
        }
        public int ID { get; set; }
        public string File_Name { get; set; }
        public string File_Code { get; set; }
        public string Topic { get; set; }
        public string Reasion { get; set; }
        public string URL { get; set; }
        public int Created_By { get; set; }
        public int Signed_By { get; set; }
        public int Approve_By { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Approve_Date { get; set; }
        public DateTime Signed_Date { get; set; }
        public bool Status { get; set; }
        public bool Seen_Status { get; set; }
        public bool Leader_Status { get; set; }
        public bool Supervisor_Status { get; set; }
        public bool Reject_Status { get; set; }
        public DateTime Seen_Time { get; set; }
        public bool Pending_Status { get; set; }
        public bool isShow { get; set; }
        public bool Complete_Status { get; set; }
        public int Delete_By { get; set; }
        public DateTime Delete_Time { get; set; }

    }
}
