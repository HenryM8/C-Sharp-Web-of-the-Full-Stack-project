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
    public class UserModel
    {
        string urlWebApi = ConfigurationManager.AppSettings["urlWebApi"].ToString();

        public int AddUser(UserEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/AddUser";
                JsonContent body = JsonContent.Create(entidad);
                HttpResponseMessage resp = client.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<int>().Result;
                }

                return 0;
            }
        }

        public List<UserEnt> GetUsers()
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/GetUsers";
                string token = HttpContext.Current.Session["Token_session"].ToString();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<UserEnt>>().Result;
                }

                return new List<UserEnt>();
            }
        }

        public UserEnt GetUser(long q)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/GetUser?q=" + q;
                string token = HttpContext.Current.Session["Token_session"].ToString();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<UserEnt>().Result;
                }

                return null;
            }
        }

        public UserEnt Login(UserEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/Login";
                JsonContent body = JsonContent.Create(entidad);
                HttpResponseMessage resp = client.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<UserEnt>().Result;
                }

                return null;
            }
        }

        public int ValidateUserData(UserEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/ValidateUserData";
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

        public bool RecoveryPassword(UserEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/RecoveryPassword";
                JsonContent body = JsonContent.Create(entidad);
                HttpResponseMessage resp = client.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<bool>().Result;
                }

                return false;
            }
        }

        public int ChangePassword(UserEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/ChangePassword";
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

        public void UpdateUserImage(UserEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/UpdateUserImage";
                string token = HttpContext.Current.Session["Token_session"].ToString();
                JsonContent body = JsonContent.Create(entidad);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.PutAsync(url, body).Result;
            }
        }
    }
}