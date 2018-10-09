using MentalStack.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MentalStack.IntegrationTests
{
    public class GenRequestControllerTests : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _fixture;

        public GenRequestControllerTests(TestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetRequest_CorrectEmptyBody_ReturnsOK()
        {
            var response = await _fixture.Client.PostAsync("/",
                new StringContent(
                    JsonConvert.SerializeObject(new GenRequest()
                        {
                            Meta = new Meta(),
                            Request = new Request()
                            { Command = "" },
                            Session = new Session(),
                            Version = ""
                        }
                    ),
                    Encoding.UTF8,
                    "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetRequest_PushRequestInBody_ReturnsSuccesMessage()
        {
            var response = await _fixture.Client.PostAsync("/",
                new StringContent(
                    JsonConvert.SerializeObject(new GenRequest()
                    {
                        Meta = new Meta(),
                        Request = new Request()
                        {
                            Command = "Положи на стек, рассказать историю про программиста",
                            OriginalUtterance = "Положи на стек, рассказать историю про программиста"
                        },
                        Session = new Session()
                        {
                            UserId = "AC9WC3DF6FCE052E45A4566A48E6B7193774B84814CE49A922E163B8B29881DC"
                        },
                        Version = ""
                    }
                    ),
                    Encoding.UTF8,
                    "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = await response.Content.ReadAsStringAsync();
            JObject jObj = JObject.Parse(json);
            string message = jObj["response"]["text"].ToString();
            Assert.Equal("Стэк создан и запись успешно добавлена", message);
        }
    }
}
