using FluentAssertions;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Xunit;

namespace OloPOC.PostsTests
{
    [Collection("Rest Client Collection")]

    public class GetTests
    {
        //note: these tests assume that there is at least 1 valid post in the output ofr get all. 
        //This is the case with the fake service, might not be true for a real one.
        RestClientFixture Fixture;

        public GetTests(RestClientFixture fixture)
        {
            Fixture = fixture;
        }


        [Fact]
        public void GetAllReturnsStatusOk()
        {
            var response = Fixture.GetAll();
            response.StatusCode.Should().Equals(HttpStatusCode.OK);
        }

        [Fact]
        public void GetAllReturnsList()
        {
            var response = Fixture.GetAll();
            SimpleJson.DeserializeObject(response.Content).Should().BeAssignableTo(typeof(IList));
        }

        [Fact]
        public void GetAllReturnsPostsSchema()
        {
            var firstPost = Fixture.GetFirstPost();
            firstPost.Keys.Should().Contain(new[] { "id","title","body","userId" });
        }

        [Fact]
        public void GetByIdReturnsStatusOk()
        {
            var firstPost = Fixture.GetFirstPost();
            var response = Fixture.GetById(firstPost["id"]);
            response.StatusCode.Should().Equals(HttpStatusCode.OK);
        }

        [Fact]
        public void GetByIdReturnsStatusNotFoundIfIdMissing()
        {
            var response = Fixture.GetById(-1);
            response.StatusCode.Should().Equals(HttpStatusCode.NotFound);
        }

        [Fact]
        public void GetByIdReturnsMatchingPost()
        {
            var firstPost = Fixture.GetFirstPost();
            var response = Fixture.GetById(firstPost["id"]);
            var actual = SimpleJson.DeserializeObject(response.Content) as IDictionary<string, object>;
            actual["id"].Should().Equals(firstPost["id"]);
            actual["title"].Should().Equals(firstPost["title"]);
            actual["body"].Should().Equals(firstPost["body"]);
            actual["userId"].Should().Equals(firstPost["userId"]);
        }

    }
}
