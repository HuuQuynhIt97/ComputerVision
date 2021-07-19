using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPVS_API.Models
{
    public class Line
    {
        //public Car()
        //{
        //    this.CreatedTime = DateTime.Now;
        //}
        public int ID { get; set; }
        public string Line_Name { get; set; }
        public bool Status { get; set; }
    }
}