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
    public class RestaurantModel
    {
        string urlWebApi = ConfigurationManager.AppSettings["urlWebApi"].ToString();

        public long AddRestaurant(RestaurantEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/AddRestaurant";
                string token = HttpContext.Current.Session["Token_session"].ToString();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                JsonContent body = JsonContent.Create(entidad);
                HttpResponseMessage resp = client.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<long>().Result;
                }

                return 0;
            }
        }

        public List<RestaurantEnt> GetRestaurants()
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/GetRestaurants";
                string token = HttpContext.Current.Session["Token_session"].ToString();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<RestaurantEnt>>().Result;
                }

                return new List<RestaurantEnt>();
            }
        }

        public RestaurantEnt GetRestaurant(long q)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/GetRestaurant?q=" + q;
                string token = HttpContext.Current.Session["Token_session"].ToString();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<RestaurantEnt>().Result;
                }

                return new RestaurantEnt();
            }
        }

        public int ChangeRestaurantVisibility(RestaurantEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/ChangeRestaurantVisibility";
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

        public int ValidateRestaurantData(RestaurantEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/ValidateRestaurantData";
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

        public void UpdateRestaurantImage(RestaurantEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/UpdateRestaurantImage";
                string token = HttpContext.Current.Session["Token_session"].ToString();
                JsonContent body = JsonContent.Create(entidad);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.PutAsync(url, body).Result;
            }
        }
    }
}