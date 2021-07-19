using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPVS_API.Models
{
    public class Time
    {
        //public Car()
        //{
        //    this.CreatedTime = DateTime.Now;
        //}
        public int ID { get; set; }
        public int Plan_ID { get; set; }
        public DateTime? Time_Start { get; set; }
        public DateTime? Time_End { get; set; }
    }
}