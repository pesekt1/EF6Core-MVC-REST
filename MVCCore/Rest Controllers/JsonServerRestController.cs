using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MVCCore.Rest_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JsonServerRestController
    {
        private string API_JSON_SERVER = "http://localhost:3001/";
        private readonly IHttpClientFactory _clientFactory;
        public bool GetPostsError { get; private set; }
        public ICollection<Post> posts { get; private set; }


        public JsonServerRestController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        
        
        // GET: api/...
        [HttpGet]
        public async Task<IEnumerable<Post>> GetCourses()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, API_JSON_SERVER + "posts");
            request.Headers.Add("Accept", "application/json");
            // request.Headers.Add("User-Agent", "HttpClientFactory-Sample");
            
            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                
                //Problem: the post objects are empty... why?
                posts = await JsonSerializer.DeserializeAsync<ICollection<Post>>(responseStream);

                return posts;
            }
            else
            {
                GetPostsError = true;
                return null;
            }
            
        }
    }
}