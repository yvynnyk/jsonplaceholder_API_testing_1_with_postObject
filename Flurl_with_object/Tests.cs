using NUnit.Framework;
using Flurl.Http;
using System;
using Newtonsoft.Json;

namespace Flurl_with_object
{
    public class Tests
    {
        private string testDomain;
        private string testFolder;
        private string testId;
        private const int testStatus200 = 200;
        private const int testStatus201 = 201;
        private Type typeData;
        private PostObject postObject;

        [OneTimeSetUp]
        public void SetUp()
        {
            testDomain = "https://jsonplaceholder.typicode.com";
            testFolder = "/posts";
            testId = "/1";
            typeData = typeof(string);
            postObject = new PostObject("Post title", "Post body", 120);
        }

        [Test]
        public void GET_Test()
        {
            var testResource = testDomain + testFolder + testId;
            var response = testResource.GetAsync().Result;
            var responseBody = response.ResponseMessage.Content.ReadAsStringAsync().Result;
            var responseStatus = response.StatusCode;

            Assert.Multiple(() =>
            {
                Assert.That(responseBody.GetType(), Is.EqualTo(typeData));
                Assert.That(responseStatus, Is.EqualTo(testStatus200));
            });
        }
        [Test]
        public void POST_Test()
        {
            var testResource = testDomain + testFolder;
            var response = testResource
                .PostJsonAsync(postObject);
            var responseJson = JsonConvert.DeserializeObject<PostObject>(response.Result.ResponseMessage.Content.ReadAsStringAsync().Result);
            var responseStatus = response.Result.StatusCode;
             
            Assert.Multiple(() =>
            {
                Assert.That(responseJson.title, Is.EqualTo(postObject.title), "Wrong title");
                Assert.That(responseJson.body, Is.EqualTo(postObject.body), "Wrong body");
                Assert.That(responseJson.userId, Is.EqualTo(postObject.userId), "Wrong userId");
                Assert.That(responseStatus, Is.EqualTo(testStatus201));
            });
        }
        [Test]
        public void PUT_Test()
        {
            var testResource = testDomain + testFolder + testId;
            var response = testResource
                .PutJsonAsync(new
                {
                    id = 5,
                    title = "Changed title",
                    body = "Changed body",
                    userId = 121,
                });
            var responseBody = response.Result.ResponseMessage.Content.ReadAsStringAsync().Result;
            var responseStatus = response.Result.StatusCode;

            Assert.Multiple(() =>
            {
                Assert.That(responseBody.GetType(), Is.EqualTo(typeData));
                Assert.That(responseStatus, Is.EqualTo(testStatus200));
            });

        }
        [Test]
        public void PATCH_Test()
        {
            var testResource = testDomain + testFolder + testId;
            var response = testResource
                .PatchJsonAsync(new
                {
                    title = "Completely new title",
                    userId = 155,
                });
            var responseBody = response.Result.ResponseMessage.Content.ReadAsStringAsync().Result;
            var responseStatus = response.Result.StatusCode;

            Assert.Multiple(() =>
            {
                Assert.That(responseBody.GetType(), Is.EqualTo(typeData));
                Assert.That(responseStatus, Is.EqualTo(testStatus200));
            });

        }
        [Test]
        public void DELETE_Test()
        {
            var testResource = testDomain + testFolder + testId;
            var response = testResource.DeleteAsync();
            var responseStatus = response.Result.StatusCode;

            Assert.That(responseStatus, Is.EqualTo(testStatus200));

        }

    }
}