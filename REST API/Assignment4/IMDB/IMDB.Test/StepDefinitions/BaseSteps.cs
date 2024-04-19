using IMDB.Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.Common;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Infrastructure;
using static Google.Apis.Requests.BatchRequest;

namespace IMDB.Test.StepDefinitions
{
    public class BaseSteps: IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {

        protected WebApplicationFactory<TestStartup> _baseFactory;
        protected HttpResponseMessage Response { get; set; }
        protected HttpClient Client { get; set; }

        public BaseSteps(WebApplicationFactory<TestStartup> factory)
        {
            _baseFactory=factory;
        }

        [Given(@"I am a Client")]
        public void GivenIAmAClient()
        {
            Client = _baseFactory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri($"http://localhost/")
            });
        }

        [When(@"I make a GET Request '(.*)'")]
        public virtual async Task WhenIMakeAGETRequest(string resourceEndpoint)
        {
            var uri = new Uri(resourceEndpoint, UriKind.Relative);
            Response = await Client.GetAsync(uri);
        }

        [Then(@"response code must be '([^']*)'")]
        public void ThenResponseCodeMustBe(int statusCode)
        {
            var expectedStatusCode = (HttpStatusCode)statusCode;
            Assert.Equal(expectedStatusCode, Response.StatusCode);
        }

        [Then(@"response should look like '([^']*)'")]
        public void ThenResponseShouldLookLike(string expectedResponseData)
        {
            var responseData = Response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Assert.Equal(expectedResponseData, responseData);
        }

        [When(@"I make a post request to '([^']*)' with the following data '([^']*)'")]
        public virtual async Task WhenIMakeAPostRequestToWithTheFollowingData(string resourceEndpoint, string requestData)
        {
            var uri = new Uri(resourceEndpoint, UriKind.Relative);
            var content = new StringContent(requestData, Encoding.UTF8, "application/json");
            Response = await Client.PostAsync(uri, content);
        }

        [When(@"I make a put request to '([^']*)' with the following data '([^']*)'")]
        public virtual async Task WhenIMakeAPutRequestTo(string resourceEndpoint,string requestData)
        {
            var uri = new Uri(resourceEndpoint, UriKind.Relative);
            var content = new StringContent(requestData, Encoding.UTF8, "application/json");
            Response = await Client.PutAsync(uri, content);
        }

        [When(@"I make a delete request to '([^']*)'")]
        public virtual async Task WhenIMakeADeleteRequestTo(string resourceEndpoint)
        {
            var uri = new Uri(resourceEndpoint, UriKind.Relative);
            Response = await Client.DeleteAsync(uri);
        }



    }
}
