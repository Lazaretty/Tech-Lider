using ApiClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApiClient
{
    class Program
    {
  
        static void Main(string[] args)
        {
            Console.WriteLine("Enter number of command:\n1-Get Albums\n2-Post Albums\n0 - exit");
            int swtch;

            string cookiesName = "AspNetCore.Cookies";

            swtch = Convert.ToInt32(Console.ReadLine());
            while (swtch != 0)
            {
                switch (swtch) 
                {
                    case 1:
                        {
                            var albums = GetAlbums();

                            foreach (var item in albums.Result)
                            {
                                Console.WriteLine("Id: " + item.Id + "\nAlbum Name: " + item.Name + "\nUser ID: " + item.UserId + "\nAmount Of Photo in album: " + item.Photos.Count + "\n-------------");
                            }
                            break;
                        }
                    case 2:
                        {
                            var payload = "{\"UserId\": 777,\"Name\": \"PepsiAlb\"}";
                            HttpContent c  = new StringContent (payload, Encoding.UTF8, "application/json");
                            var result = PostAlbums(c);
                            Console.WriteLine(result.Result);
                            break;
                        }
                    case 3:
                        {
                            var user = new User() {Email = "dima123", Password="123"};
                            string jsonString;
                            jsonString = JsonSerializer.Serialize(user);
                            HttpContent c = new StringContent(jsonString);
                            //HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
                            var result = Login(c, cookiesName);
                            Console.WriteLine(result.Result);

                            break;
                        }

                }

                swtch = Convert.ToInt32(Console.ReadLine());
            }
            
        }

        static async Task<List<Album>> GetAlbums()
        {
            var albums = new List<Album>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44338/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Albums");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    albums = await response.Content.ReadAsAsync<List<Album>>();
                }
            }

            return albums;
        }


        static async Task<string> PostAlbums(HttpContent c)
        {
            var response = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44338/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage result = await client.PostAsync("api/Albums",c);
                
                if (result.IsSuccessStatusCode)
                {
                    response = result.StatusCode.ToString();
                }
            }

            return response;
        }


        static async Task<string> Login(HttpContent c, string cookieName)
        {
            var response = string.Empty;
            var uri = new Uri("https://localhost:44338/");
            var cookieContainer = new CookieContainer();
            using (var httpClientHandler = new HttpClientHandler
            {
                CookieContainer = cookieContainer
            })
            using (HttpClient client = new HttpClient(httpClientHandler))
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                

                HttpResponseMessage result = await client.PostAsync("api/Login", c);
                var cookie = cookieContainer.GetCookies(uri).Cast<Cookie>().FirstOrDefault(x => x.Name == cookieName);

                return cookie?.Value;
            }

           
        }
            

    }
}
