using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPVS_API.Models
{
    public class Car
    {
        //public Car()
        //{
        //    this.CreatedTime = DateTime.Now;
        //}
        public int ID { get; set; }
        public string Car_name { get; set; }
    }
}