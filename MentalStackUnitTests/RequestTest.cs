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
                Command = "������ �� ���� �� ���� ��������� ������ ������"
            };
            var workRequestType = request.CreateWorkRequest("");
            Assert.IsType<PushRequest>(workRequestType);
        }

        [Fact]
        public void CreateWorkRequest_PullFromStackCommand_RequestTypeIsPopRequest()
        {
            var request = new Request()
            {
                Command = "������ �� ����� ������� ������"
            };
            var workRequestType = request.CreateWorkRequest("");
            Assert.IsType<PopRequest>(workRequestType);
        }

        [Fact]
        public void CreateWorkRequest_UnknownCommand_RequestTypeIsUnknownRequest()
        {
            var request = new Request()
            {
                Command = "������� ��������� ����"
            };
            var workRequestType = request.CreateWorkRequest("");
            Assert.IsType<UnknownRequest>(workRequestType);
        }
    }
}
