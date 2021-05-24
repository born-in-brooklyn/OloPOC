using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OloPOC.PostsTests
{
    public class RestClientFixture
    {
        public RestClient Client { get; private set; }

        public RestClientFixture()
        {
            Client = new RestClient("https://jsonplaceholder.typicode.com");
        }

        public IRestResponse GetAll()
        {
            var request = new RestRequest("posts", Method.GET);
            return Client.Execute(request);
        }

        public IRestResponse GetById(object id)
        {
            var request = new RestRequest("posts/{id}", Method.GET);
            request.AddUrlSegment("id", id);
            return Client.Execute(request);
        }

        public IDictionary<string, object> GetFirstPost()
        {
            var response = GetAll();
            return (IDictionary<string, object>)(SimpleJson.DeserializeObject(response.Content) as JsonArray)?.FirstOrDefault();
        }
    }
}
