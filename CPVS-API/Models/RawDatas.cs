using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CPVS_API.Models
{
    public class RawDatas
    {
        public RawDatas()
        {
        }
        public int ID { get; set; }
        public DateTime TimeRecieve { get; set; }
        public int Count { get; set; }
        public string Line { get; set; }
        public string Camera { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
