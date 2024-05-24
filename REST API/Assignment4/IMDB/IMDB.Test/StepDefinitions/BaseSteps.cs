
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace IMDB.Test.StepDefinitions
{
    public class BaseSteps: IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {

        private WebApplicationFactory<TestStartup> _baseFactory;
        private HttpResponseMessage _response { get; set; }
        private HttpClient _client { get; set; }

        public BaseSteps(WebApplicationFactory<TestStartup> factory)
        {
            _baseFactory=factory;
        }

        [Given(@"I am a Client")]
        public void GivenIAmAClient()
        {
            _client = _baseFactory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri($"http://localhost/")
            });
        }

        [When(@"I make a GET Request '(.*)'")]
        public virtual async Task WhenIMakeAGETRequest(string resourceEndpoint)
        {
            var uri = new Uri(resourceEndpoint, UriKind.Relative);
            _response = await _client.GetAsync(uri);
        }

        [Then(@"response code must be '([^']*)'")]
        public void ThenResponseCodeMustBe(int statusCode)
        {
            var expectedStatusCode = (HttpStatusCode)statusCode;
            Assert.Equal(expectedStatusCode, _response.StatusCode);
        }

        [Then(@"response should look like '([^']*)'")]
        public void ThenResponseShouldLookLike(string expectedResponseData)
        {
            var responseData = _response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Assert.Equal(expectedResponseData, responseData);
        }

        [When(@"I make a post request to '([^']*)' with the following data '([^']*)'")]
        public virtual async Task WhenIMakeAPostRequestToWithTheFollowingData(string resourceEndpoint, string requestData)
        {
            var uri = new Uri(resourceEndpoint, UriKind.Relative);
            var content = new StringContent(requestData, Encoding.UTF8, "application/json");
            _response = await _client.PostAsync(uri, content);
        }

        [When(@"I make a put request to '([^']*)' with the following data '([^']*)'")]
        public virtual async Task WhenIMakeAPutRequestTo(string resourceEndpoint,string requestData)
        {
            var uri = new Uri(resourceEndpoint, UriKind.Relative);
            var content = new StringContent(requestData, Encoding.UTF8, "application/json");
            _response = await _client.PutAsync(uri, content);
        }

        [When(@"I make a delete request to '([^']*)'")]
        public virtual async Task WhenIMakeADeleteRequestTo(string resourceEndpoint)
        {
            var uri = new Uri(resourceEndpoint, UriKind.Relative);
            _response = await _client.DeleteAsync(uri);
        }



    }
}
