using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace api1.Controllers
{
    [Produces("application/json")]
    [Route("api/Claims")]
    public class ClaimsController : Controller
    {
        // GET api/values
        [HttpGet]
        [Authorize]
        public Dictionary<string, List<string>> Get()
        {
            Dictionary<string, List<string>> rtnDict = new Dictionary<string, List<string>>();

            foreach (var c in User.Claims)
            {
                var key = c.Type;
                if (!rtnDict.ContainsKey(key))
                {
                    //add
                    rtnDict.Add(key, new List<string>());
                }
                rtnDict[key].Add(c.Value);
            }

            return rtnDict;
        }
    }

   
}