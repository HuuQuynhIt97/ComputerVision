using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CPVS_API.Models
{
    public class Test
    {
        public Test()
        {
            this.Created_Time = DateTime.Now;
        }
        [Key]
        public int ID { get; set; }
        public string test { get; set; }
        public DateTime? Created_Time { get; set; }
    }
}