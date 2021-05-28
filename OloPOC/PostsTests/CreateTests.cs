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

    public class CreateTests
    {
        RestClientFixture Fixture;

        public CreateTests(RestClientFixture fixture)
        {
            Fixture = fixture;
        }

        private object BuildDefaultNewPost()
        {
            return new {
                title = "Some Title",
                body = "Some Body",
                userId = 12345,
            };
        }

        [Fact]
        public void CreateReturnsStatusCreated()
        {
            var response = Fixture.Create(BuildDefaultNewPost());
            response.StatusCode.Should().Equals(HttpStatusCode.Created);
        }


        [Fact]
        public void CreateReturnsExpectedPost()
        {
            dynamic expected = BuildDefaultNewPost();
            var response = Fixture.Create(expected);
            var actual = SimpleJson.DeserializeObject(response.Content) as IDictionary<string, object>;
            actual["id"].Should().NotBeNull();
            actual["title"].Should().Equals(expected.title);
            actual["body"].Should().Equals(expected.body);
            actual["userId"].Should().Equals(expected.userId);
        }

        //todo: since the system under test is fake, we can't build input validation tests, but we should once the system is real
    }
}
