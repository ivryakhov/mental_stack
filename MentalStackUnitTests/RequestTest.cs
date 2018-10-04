using MentalStack.Entities;
using System;
using Xunit;

namespace MentalStackUnitTests
{
    public class RequestTest
    {
        [Fact]
        public void CreateWorkRequest_PushOnStackCommand_RequestTypeIsPushRequest()
        {
            var request = new Request()
            {
                Command = "Положи на стек Во всей вселенной пахнет нефтью"
            };
            var workRequestType = request.CreateWorkRequest("");
            Assert.IsType<PushRequest>(workRequestType);
        }

        [Fact]
        public void CreateWorkRequest_PullFromStackCommand_RequestTypeIsPopRequest()
        {
            var request = new Request()
            {
                Command = "Возьми со стека прошлую запись"
            };
            var workRequestType = request.CreateWorkRequest("");
            Assert.IsType<PopRequest>(workRequestType);
        }

        [Fact]
        public void CreateWorkRequest_UnknownCommand_RequestTypeIsUnknownRequest()
        {
            var request = new Request()
            {
                Command = "Напомни покормить кота"
            };
            var workRequestType = request.CreateWorkRequest("");
            Assert.IsType<UnknownRequest>(workRequestType);
        }
    }
}
