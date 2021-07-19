using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CPVS_API.Helpers
{

    public class Appsettings
    {
        public string Token { get; set; }
        public string API_AUTH_URL { get; set; }
        public int SystemCode { get; set; }
        public string[] CorsPolicy { get; set; }
    }

}

