using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GreenHealth.ApiChannel;
using GreenHealth.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace GreenHealth.Controllers
{
    public class DoctorController : Controller
    {
        GreenHealth.ApiChannel.ApiChannel api = new ApiChannel.ApiChannel();
        public async Task<IActionResult> Index()
        {
            List<Doctor> doctor = new List<Doctor>();
            HttpClient client = api.httpClient();
            HttpResponseMessage res = await client.GetAsync("api/doctor");
           if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                doctor = JsonConvert.DeserializeObject<List<Doctor>>(result);
            }
            return View(doctor);
        }
    }
}