using CGWEB.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Web;

namespace CGWEB.Models
{
    public class ReviewModel
    {
        string urlWebApi = ConfigurationManager.AppSettings["urlWebApi"].ToString();

        public int AddReview(ReviewEnt reviewEnt)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/AddReview";
                string token = HttpContext.Current.Session["Token_session"].ToString();

                JsonContent body = JsonContent.Create(reviewEnt);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<int>().Result;
                }

                return 0;
            }
        }

        public List<ReviewEnt> GetReviews()
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/GetReviews";
                string token = HttpContext.Current.Session["Token_session"].ToString();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<ReviewEnt>>().Result;
                }

                return new List<ReviewEnt>();
            }
        }

        public ReviewEnt GetReview(long q)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/GetReview?q=" + q;
                string token = HttpContext.Current.Session["Token_session"].ToString();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<ReviewEnt>().Result;
                }

                return null;
            }
        }

        public List<ReviewEnt> GetReviewsByRestaurantId(long q)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/GetReviewsByRestaurantId?q=" + q;
                string token = HttpContext.Current.Session["Token_session"].ToString();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<ReviewEnt>>().Result;
                }

                return new List<ReviewEnt>();
            }
        }

        public int ChangeReviewVisibility(ReviewEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/ChangeReviewVisibility";
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

        public List<ReviewEnt> GetReviewsByUser(long q)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/GetReviewsByUser?q=" + q;
                string token = HttpContext.Current.Session["Token_session"].ToString();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<ReviewEnt>>().Result;
                }

                return new List<ReviewEnt>();
            }
        }

        public int ValidateReviewData(ReviewEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/ValidateReviewData";
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
        public int DeleteReviewData(long q)
        {
            using (var client = new HttpClient())
            {
                string url = urlWebApi + "api/DeleteReviewtData?q=" + q;
                string token = HttpContext.Current.Session["Token_session"].ToString();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage resp = client.DeleteAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<int>().Result;
                }

                return 0;
            }
        }
    }
}