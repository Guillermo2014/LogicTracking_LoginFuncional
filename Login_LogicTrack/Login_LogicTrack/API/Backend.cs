using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Login_LogicTrack.API
{
    public class Backend
    {
        public HttpClient Initial()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("https://upc-tracking-backend.herokuapp.com/api/");
            return Client;
        }
    }
}