using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPVS_API.Models
{
    public class Plan
    {
        public Plan()
        {
            this.CreatedDate = DateTime.Now;
            this.CreatedDateTime = DateTime.Now;
        }
        public int ID { get; set; }
        public int Line_ID { get; set; }
        public string Line_Name { get; set; }
        public int Car_ID { get; set; }
        public int Amount { get; set; }
        public string Car_Name { get; set; }
        public string PO { get; set; }
        public bool? Status { get; set; }
        public bool IsStart { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? Time_Start { get; set; }
        public DateTime? Time_End { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CreatedDateTime { get; set; }
    }
}