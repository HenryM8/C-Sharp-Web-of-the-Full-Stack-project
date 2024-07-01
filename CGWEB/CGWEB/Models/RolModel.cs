using CGWEB.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Web;
using System.Net.Http.Headers;

namespace CGWEB.Models
{
    public class RolModel
    {
        string urlWebApi = ConfigurationManager.AppSettings["urlWebApi"].ToString();

        public int AddRol(RolEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/AddRol";
                string token = HttpContext.Current.Session["Token_session"].ToString();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                JsonContent body = JsonContent.Create(entidad);
                HttpResponseMessage resp = client.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<int>().Result;
                }

                return 0;
            }
        }

        public List<RolEnt> GetRoles()
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/GetRoles";
                string token = HttpContext.Current.Session["Token_session"].ToString();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<RolEnt>>().Result;
                }

                return new List<RolEnt>();
            }
        }

        public RolEnt GetRol(long q)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/GetRol?q=" + q;
                string token = HttpContext.Current.Session["Token_session"].ToString();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<RolEnt>().Result;
                }

                return null;
            }
        }

        public int ValidateRolData(RolEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/ValidateRolData";
                string token = HttpContext.Current.Session["Token_session"].ToString();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                JsonContent body = JsonContent.Create(entidad);
                HttpResponseMessage resp = client.PutAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<int>().Result;
                }

                return 0;
            }
        }
    }
}