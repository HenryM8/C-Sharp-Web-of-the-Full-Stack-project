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
    public class TagModel
    {
        string urlWebApi = ConfigurationManager.AppSettings["urlWebApi"].ToString();

        public int AddTag(TagEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/AddTag";
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

        public List<TagEnt> GetTags()
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/GetTags";
                string token = HttpContext.Current.Session["Token_session"].ToString();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<TagEnt>>().Result;
                }

                return new List<TagEnt>();
            }
        }

        public TagEnt GetTag(long q)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/GetTag?q=" + q;
                string token = HttpContext.Current.Session["Token_session"].ToString();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<TagEnt>().Result;
                }

                return null;
            }
        }

        public int ValidateTagData(TagEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/ValidateTagData";
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