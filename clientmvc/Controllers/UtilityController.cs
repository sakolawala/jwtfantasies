using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace clientmvc.Controllers
{
    public class UtilityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetSHA256(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return Json(new { error = "Empty Input" });
            }
            else
            {
                using (var sha = SHA256.Create())
                {
                    var bytes = Encoding.UTF8.GetBytes(input);
                    var hash = sha.ComputeHash(bytes);

                    return Json(Convert.ToBase64String(hash));

                }
            }
        }
    }
}