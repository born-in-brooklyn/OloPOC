using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OloPOC.PostsTests
{
    public class RestClientFixture
    {
        private RestClient Client;

        public RestClientFixture()
        {
            Client = new RestClient("https://jsonplaceholder.typicode.com");
        }

        private IRestResponse Execute(RestRequest request)
        {
            request.AddHeader("Authorization", "Olo-token");
            return Client.Execute(request);
        }

        public IRestResponse GetAll()
        {
            var request = new RestRequest("posts", Method.GET);
            return Execute(request);
        }

        public IRestResponse GetById(object id)
        {
            var request = new RestRequest("posts/{id}", Method.GET);
            request.AddUrlSegment("id", id);
            return Execute(request);
        }

        public IDictionary<string, object> GetFirstPost()
        {
            var response = GetAll();
            return (IDictionary<string, object>)(SimpleJson.DeserializeObject(response.Content) as JsonArray)?.FirstOrDefault();
        }

        public IRestResponse Delete(object postId)
        {
            var request = new RestRequest("posts/{id}", Method.DELETE);
            request.AddUrlSegment("id", postId);
            return Execute(request);
        }

        public IRestResponse Create(object newPost)
        {
            var request = new RestRequest("posts", Method.POST);
            request.AddJsonBody(newPost);
            return Client.Execute(request);
        }
    }
}
