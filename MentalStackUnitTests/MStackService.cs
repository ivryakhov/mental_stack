using Moq;
using System;
using MentalStack.Services;
using Xunit;
using Microsoft.Extensions.Caching.Memory;
using MentalStack.Entities;
using System.Collections.Generic;

namespace MentalStackUnitTests
{
    public class MStackServiceTests
    {
        [Fact]
        public void Push_UserDoesNotExistBefore_CreatesNewMStackInDb()
        {
            var user = "TestUser";
            var message = "Test message";
            var messages = new Stack<string>();
            messages.Push(message);
            var mStack = new MStack { User = user, Messages = messages };

            var memoryCache =new Mock<IMemoryCache>();
            var cachEntry = Mock.Of<ICacheEntry>();

            memoryCache
                .Setup(m => m.CreateEntry(It.IsAny<object>()))
                .Returns(cachEntry);

            var mStackService = new MStackService(memoryCache.Object);
            var result = mStackService.Push(user, message);

            Assert.Equal("OK", result);
            memoryCache.Verify(x => x.CreateEntry(user));
        }
    }
}
