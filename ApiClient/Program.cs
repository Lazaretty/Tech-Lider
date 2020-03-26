using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using TechLider.Models;

namespace ApiClient
{
    class Program
    {
        private readonly HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            Console.WriteLine("Enter number of command:" +
                    "\n1-Get Albums" +
                    "\n2-Post Albums" +
                    "\n3-Update Album" +
                    "\n4-Delete Album" +
                    "\n5-Get Photos" +
                    "\n6-Post Photo" +
                    "\n7-Delete Photo" +
                    "\n8-Update Photo" +
                    "\n9-Login" +
                    "\n10-Register" +
                    "\n0 - exit");

            var @switch = Convert.ToInt32(Console.ReadLine());
            while (@switch != 0)
            {
                switch (@switch) 
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

                            Console.WriteLine("Enter NAME of album and user Id");
                            var name = Console.ReadLine();
                            var userId = Convert.ToInt32(Console.ReadLine());
                            var album = new Album() { Name = name, UserId = userId };
                            var result = PostAlbums(album);
                            Console.WriteLine(result.Result);
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Enter Id album to uptdate");
                            var albumId = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter NAME of album and user Id");
                            var name = Console.ReadLine();
                            var userId = Convert.ToInt32(Console.ReadLine());
                            var album = new Album() { Name = name, UserId = userId, Id = albumId };
                            var result = PutAlbum(albumId, album);
                            Console.WriteLine(result.Result);
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("Enter Id album to deletion");
                            var albumId = Convert.ToInt32(Console.ReadLine());
                            var result = DeleteAlbum(albumId);
                            Console.WriteLine(result.Result);
                            break;

                        }
                    case 5: 
                        {
                            var photos = GetPhotos();
                            foreach (var item in photos.Result)
                            {
                                Console.WriteLine("Id: " + item.Id + "\nPhoto Name: " + item.Name + "\nUser ID: " + item.AuthorId + "\nCamera Name: " + item.CameraName + "\nShoting Parametrs: " + item.ShootingParameters +"\n-------------");
                            }
                            break;
                        }
                    case 6:
                        {
                            Console.WriteLine("Enter NAME, UserId, Camera Name, Shoting Parameters of photo");
                            var name = Console.ReadLine();
                            var userId = Convert.ToInt32(Console.ReadLine());
                            var cameraName = Console.ReadLine();
                            var shootingParameters = Console.ReadLine();
                            var photo = new Photo() { Name = name, AuthorId = userId, ShootingParameters= shootingParameters, CameraName = cameraName };
                            var result = PostPhoto(photo);
                            Console.WriteLine(result.Result);

                            break;
                        }
                    case 7:
                        {
                            Console.WriteLine("Enter Photo ID to deletion");
                            var photoId = Convert.ToInt32(Console.ReadLine());
                            var result = DeletePhoto(photoId);
                            Console.WriteLine(result.Result);
                            break;

                        }
                    case 8:
                        {
                            Console.WriteLine("Enter Photo Id to uptdate");
                            var photoId = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter NAME, UserId, Camera Name, Shoting Parameters of photo");
                            var name = Console.ReadLine();
                            var userId = Convert.ToInt32(Console.ReadLine());
                            var cameraName = Console.ReadLine();
                            var shootingParameters = Console.ReadLine();
                            var photo = new Photo() { Name = name, AuthorId = userId, ShootingParameters = shootingParameters, CameraName = cameraName, Id = photoId};
                            var result = PutPhoto(photoId, photo);
                            Console.WriteLine(result.Result);
                            break;
                        }
                    
                    case 9:
                        {
                            Console.WriteLine("Enter email and Password");
                            var email = Console.ReadLine();
                            var password = Console.ReadLine();
                            var user = new User() { Id = 1, Email = email, Password = password };
                            var result = Login(user);
                            break;
                        }
                    case 10:
                        {
                            Console.WriteLine("Enter email and Password");
                            var email = Console.ReadLine();
                            var password = Console.ReadLine();
                            var user = new User() { Email = email, Password = password };
                            var result = Register(user);
                            break;
                        }

                }

                Console.WriteLine("__________________________________________");
                Console.WriteLine("\nEnter number of command:" +
                    "\n1-Get Albums" +
                    "\n2-Post Albums" +
                    "\n3-Update Album" +
                    "\n4-Delete Album" +
                    "\n5-Get Photos" +
                    "\n6-Post Photo" +
                    "\n7-Delete Photo" +
                    "\n8-Update Photo" +
                    "\n9-Login" +
                    "\n10-Register" +
                    "\n0 - exit");
                @switch = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
            }
            
        }

        public static Uri uri = new Uri("https://localhost:44338/");
        static async Task<List<Album>> GetAlbums()
        {
            var albums = new List<Album>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("api/Albums");
                response.EnsureSuccessStatusCode();
               
                albums = await response.Content.ReadAsAsync<List<Album>>();
                
            }

            return albums;
        }

        static async Task<string> PostAlbums(Album album)
        {
            var response = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var result = await client.PostAsJsonAsync("api/Albums", album);
            }

            return response;
        }

        static async Task<string> DeleteAlbum(int albumId)
        {
            var response = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var result = await client.DeleteAsync("api/Albums/"+albumId.ToString());
                response = result.StatusCode.ToString();
                    
            }

            return response;
        }

        static async Task<string> PutAlbum(int albumId, Album album)
        {
            var response = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var result = await client.PutAsJsonAsync("api/Albums/" + albumId.ToString(), album);
                response = result.StatusCode.ToString();

            }

            return response;
        }

        static  IEnumerable<Cookie> Login(User user)
        {

            var cookies = new CookieContainer();
            var handler = new HttpClientHandler();
            handler.CookieContainer = cookies;
            HttpClient authClient = new HttpClient(handler);
            authClient.BaseAddress = uri;
            authClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var authenticationResponse = authClient.PostAsJsonAsync("api/Login", user).Result;
            var responseCookies = cookies.GetCookies(uri).Cast<Cookie>();

            return responseCookies;
        }

        static async Task<string> Register(User user)
        {
            var response = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var result = await client.PostAsJsonAsync("api/Register", user);

            }

            return response;
        }

        static async Task<List<Photo>> GetPhotos()
        {
            var photos = new List<Photo>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("api/Photos");
                response.EnsureSuccessStatusCode();

                photos = await response.Content.ReadAsAsync<List<Photo>>();

            }

            return photos;
        }

        static async Task<Photo> GetPhotoById(int photoid)
        {
            var photo = new Photo();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("api/Photos/"+photoid.ToString());
                response.EnsureSuccessStatusCode();

                photo = await response.Content.ReadAsAsync<Photo>();

            }

            return photo;
        }

        static async Task<string> PostPhoto(Photo photo)
        {
            var response = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var result = await client.PostAsJsonAsync("api/Photos", photo);

            }

            return response;
        }

        static async Task<string> DeletePhoto(int photoId)
        {
            var response = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var result = await client.DeleteAsync("api/Photos/" + photoId.ToString());


                response = result.StatusCode.ToString();

            }

            return response;
        }

        static async Task<string> PutPhoto(int photoId, Photo photo)
        {
            var response = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var result = await client.PutAsJsonAsync("api/Photos/" + photoId.ToString(), photo);


                response = result.StatusCode.ToString();

            }

            return response;
        }

    }
}
