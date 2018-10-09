using MentalStack.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
           
        }
    }
}
