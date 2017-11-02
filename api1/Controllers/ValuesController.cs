using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using api1.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace api1.Controllers
{    
    [Route("api/[controller]")]
    [Authorize()]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        [SecurityLevel(4)]        
        public IEnumerable<string> Get()
        {            
            return new string[] { "value1", "value2" };
        }

    }
}
