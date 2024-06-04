using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;

namespace Specflow_restSharp.Steps
{
    [Binding]
    public sealed class JsonPlaceHolderSteps
    {
        private RestClient _restClient;
        private RestResponse _response;
        private JObject _payload;
        private RestRequest _restRequest;

        public JsonPlaceHolderSteps()
        {
        }

        [Given("the API endpoint is \"(.*)\"")]
        public void GivenTheApiEndpointIs(string url)
        {
            _restClient = new RestClient(url);
            _payload = new JObject();
        }

        [Given("the request body contains \"(.*)\" with value \"(.*)\"")]
        public void GivenTheRequestBodyContainsWithValue(string key, string value)
        {
            _payload.Add(key, value);
        }

        [When("a POST request is sent")]
        public async Task WhenAPostRequestIsSent()
        {
            _restRequest = new RestRequest();
            _restRequest.AddStringBody(_payload.ToString(), DataFormat.Json);
            _response = await _restClient.PostAsync(_restRequest);
        }

        [Then("the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int statusCode)
        {
            TestContext.Progress.WriteLine("\nExpected Status Code: " + statusCode + "\n");
            TestContext.Progress.WriteLine("\nActual Status Code: " + (int)_response.StatusCode + "\n");
            TestContext.Progress.WriteLine("\nActual Response: " + _response.Content + "\n");
            Assert.AreEqual(statusCode, Convert.ToInt32(_response.StatusCode));
        }

        [Then("the response should contain the \"(.*)\" with value \"(.*)\"")]
        public void ThenTheResponseShouldContainWithValue(string key, string expectedValue)
        {
            var jsonResponse = JObject.Parse(_response.Content);
            var actualValue = jsonResponse[key]?.ToString();
            TestContext.Progress.WriteLine($"\nExpected {key}: {expectedValue}\n");
            TestContext.Progress.WriteLine($"\nActual {key}: {actualValue}\n");
            Assert.AreEqual(expectedValue, actualValue);
        }

    }
}