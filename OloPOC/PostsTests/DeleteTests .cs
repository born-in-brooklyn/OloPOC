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

    public class DeleteTests
    {
        RestClientFixture Fixture;

        public DeleteTests(RestClientFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact]
        public void DeleteReturnsStatusOk()
        {
            var response = Fixture.Delete(Fixture.GetFirstPost()["id"]);
            response.StatusCode.Should().Equals(HttpStatusCode.OK);
        }

        //todo: since the system under test is fake, we can't build delete validation tests, but we should once the system is real
    }
}
