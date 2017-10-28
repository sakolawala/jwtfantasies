using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using clientmvc.Models;
using RestSharp;

namespace clientmvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetOAuthToken(string url)
        {
            var client = new RestClient(url);
            var request = new RestRequest("connect/token", Method.POST);
            request.AddHeader("x-api-key", "03ed12b14712499083d5aca24f56aed7");
            IRestResponse response = client.ExecuteAsync2(request).Result;
            var token = response.Content;
            return Json(token);
        }

        [HttpGet]
        public JsonResult GetDataFromService(string url, string token)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            var bearertoken = "Bearer " + token;
            request.AddHeader("Authorization", bearertoken);
            IRestResponse response = client.ExecuteAsync2(request).Result;
            var data = response.Content;
            return Json(data);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }

    public static class RestClientExtensions
    {
        public static async Task<RestResponse> ExecuteAsync2(this RestClient client, RestRequest request)
        {
            TaskCompletionSource<IRestResponse> taskCompletion = new TaskCompletionSource<IRestResponse>();
            RestRequestAsyncHandle handle = client.ExecuteAsync(request, r => taskCompletion.SetResult(r));
            return (RestResponse)(await taskCompletion.Task);
        }
    }
}
