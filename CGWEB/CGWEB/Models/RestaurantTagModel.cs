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
    public class RestaurantTagModel
    {
        string urlWebApi = ConfigurationManager.AppSettings["urlWebApi"].ToString();

        public int AddRestaurantTag(RestaurantTagEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/AddRestaurantTag";
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

        public List<RestaurantTagEnt> GetRestaurantTags()
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/GetRestaurantTags";
                string token = HttpContext.Current.Session["Token_session"].ToString();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<RestaurantTagEnt>>().Result;
                }

                return new List<RestaurantTagEnt>();
            }
        }

        public RestaurantTagEnt GetRestaurantTag(long q)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/GetRestaurantTag?q=" + q;
                string token = HttpContext.Current.Session["Token_session"].ToString();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<RestaurantTagEnt>().Result;
                }

                return null;
            }
        }
        public List<RestaurantTagEnt> GetRestaurantTagsByRestaurantId(long q)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/GetRestaurantTagsByRestaurantId?q=" + q;
                string token = HttpContext.Current.Session["Token_session"].ToString();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<RestaurantTagEnt>>().Result;
                }

                return new List<RestaurantTagEnt>();
            }
        }

        public int ValidateRestaurantTagData(RestaurantTagEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/ValidateRestaurantTagData";
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