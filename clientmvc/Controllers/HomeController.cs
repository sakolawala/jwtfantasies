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
        public JsonResult GetOAuthToken(string url, int granttype)
        {
            var client = new RestClient(url);
            var request = new RestRequest("connect/token", Method.POST);
            if (granttype == 0)
            {
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                //Client Credential Workflow
                request.AddParameter("grant_type", "client_credentials");
                request.AddParameter("scope", "api1");
                request.AddParameter("client_id", "client");
                request.AddParameter("client_secret", "secret");
            }
            else if (granttype == 1)
            {
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                //Password Grant Flow
                request.AddParameter("grant_type", "password");
                request.AddParameter("username", "dmullins");
                request.AddParameter("password", "cex123");
                request.AddParameter("scope", "api1 offline_access");
                request.AddParameter("client_id", "roclient");
                request.AddParameter("client_secret", "secret");
            }
            else
            {
                request.AddHeader("x-api-key", "03ed12b14712499083d5aca24f56aed7");
            }
            
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
            var data = "";
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                data = response.Content;
            else
                data = response.StatusDescription;
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
