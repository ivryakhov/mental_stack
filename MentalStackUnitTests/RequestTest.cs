using mental_stack.Entities;
using System;
using Xunit;

namespace MentalStackUnitTests
{
    public class RequestTest
    {
        [Fact]
        public void CreateRequest_PushOnStackCommand_RequestKindIsPush()
        {
            var request = new Request("������ �� ���� ������ ����� �������");
            Assert.Equal(Request.RequestKind.Push, request.Kind);
        }

        [Fact]
        public void CreateRequest_PullFromStackCommand_RequestKindIsPop()
        {
            var request = new Request("������ �� ����� ������� ������");
            Assert.Equal(Request.RequestKind.Pop, request.Kind);
        }

        [Fact]
        public void CreateRequest_UnknownCommand_RequestKindIsUnknown()
        {
            var request = new Request("������� ��������� ����");
            Assert.Equal(Request.RequestKind.Unknown, request.Kind);
        }
    }
}
