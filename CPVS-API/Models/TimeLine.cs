using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CPVS_API.Models
{
    public class TimeLine
    {
        public TimeLine()
        {
            this.Created_Time = DateTime.Now;
        }
        [Key]
        public int TimeLine_ID { get; set; }
        public string TimeLine_Action { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Role_Name { get; set; }
        public int ToDoList_ID { get; set; }
        public DateTime? Signed_Time { get; set; }
        public DateTime? Approve_Time { get; set; }
        public DateTime? Reject_Time { get; set; }
        public DateTime? Created_Time { get; set; }
    }
}