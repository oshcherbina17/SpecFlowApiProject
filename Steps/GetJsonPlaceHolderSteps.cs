using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;
using System;
using System.Threading.Tasks;

namespace Specflow_restSharp.Steps
{
    [Binding]
    public sealed class GetJsonPlaceHolderSteps
    {
        private RestClient _restClient;
        private RestResponse _response;
        private RestRequest _restRequest;

        public GetJsonPlaceHolderSteps()
        {
        }

        [Given("API endpoint \"(.*)\"")]
        public void GivenAPIendpoint(string url)
        {
            _restClient = new RestClient(url);
        }

        [When("a GET request is sent")]
        public async Task WhenAGetRequestIsSent()
        {
            _restRequest = new RestRequest();
            _restRequest.Method = Method.Get; // Set the HTTP method separately
            _response = await _restClient.ExecuteAsync(_restRequest);
        }


        [Then("response status should be (.*)")]
        public void ThenResponseStatusShouldBe(int statusCode)
        {
            TestContext.Progress.WriteLine("\nExpected Status Code: " + statusCode + "\n");
            TestContext.Progress.WriteLine("\nActual Status Code: " + (int)_response.StatusCode + "\n");
            TestContext.Progress.WriteLine("\nActual Response: " + _response.Content + "\n");
            Assert.AreEqual(statusCode, Convert.ToInt32(_response.StatusCode));
        }

        [Then("response should contain \"(.*)\" with value \"(.*)\"")]
        public void ThenResponseShouldContainWithValue(string key, string expectedValue)
        {
            var jsonResponse = JObject.Parse(_response.Content);
            var actualValue = jsonResponse[key]?.ToString();
            TestContext.Progress.WriteLine($"\nExpected {key}: {expectedValue}\n");
            TestContext.Progress.WriteLine($"\nActual {key}: {actualValue}\n");
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Then("response should contain non-empty \"(.*)\"")]
        public void ThenResponseShouldContainNonEmpty(string key)
        {
            var jsonResponse = JObject.Parse(_response.Content);
            var actualValue = jsonResponse[key]?.ToString();
            TestContext.Progress.WriteLine($"\nActual {key}: {actualValue}\n");
            Assert.IsFalse(string.IsNullOrEmpty(actualValue));
        }
    }
}
