using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GreenHealth.ApiChannel
{
    public class ApiChannel
    {
        public HttpClient httpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri
                ("https://api.betterdoctor.com/2016-03-01/doctors?location=37.773%2C-122.413%2C100&user_location=37.773%2C-122.413&skip=0&limit=10&user_key=676a64744fdd83312ee9eb4cbf5ee746");
            return client;
        }
    }
}
