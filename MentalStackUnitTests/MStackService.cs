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
        private Mock<IMemoryCache> _memoryCacheMock;
        private ICacheEntry _cacheEntry;
        private const string user = "TestUser";
        private const string message = "Test message";

        public MStackServiceTests()
        {
            _memoryCacheMock = new Mock<IMemoryCache>();
            _cacheEntry = Mock.Of<ICacheEntry>();
            _memoryCacheMock
                .Setup(m => m.CreateEntry(It.IsAny<object>()))
                .Returns(_cacheEntry);
        }

        [Fact]
        public void Push_UserDoesNotExistBefore_CreatesNewMStackInDb()
        {
            var mStackService = new MStackService(_memoryCacheMock.Object);
            var result = mStackService.Push(user, message);
            object expectedValue = null;

            _memoryCacheMock
                .Setup(m => m.TryGetValue(It.IsAny<string>(), out expectedValue))
                .Returns(false);

            Assert.Equal(MStackService.ResultType.UserAdded, result);
            _memoryCacheMock.Verify(x => x.CreateEntry(user));
        }

        [Fact]
        public void Push_UserAlreadyExists_UpdateMStackForTheUser()
        {
            var messages = new Stack<string>();
            messages.Push(message);
            var mStack = new MStack { User = user, Messages = messages };
            object expectedValue = mStack;   
            
            _memoryCacheMock
                .Setup(m => m.TryGetValue(It.IsAny<string>(), out expectedValue))
                .Returns(true);

            var mStackService = new MStackService(_memoryCacheMock.Object);
            var result = mStackService.Push(user, message);

            Assert.Equal(MStackService.ResultType.UserUpdated, result);
            _memoryCacheMock.Verify(x => x.CreateEntry(user));
        }

        [Fact]
        public void Pop_UserDoesNotExist_ReturnNoStack()
        {
            string expectedMessage;
            object expectedValue = null;

            _memoryCacheMock
                .Setup(m => m.TryGetValue(It.IsAny<string>(), out expectedValue))
                .Returns(false);

            var mStackService = new MStackService(_memoryCacheMock.Object);
            var result = mStackService.Pop(user, out expectedMessage);

            Assert.Equal(MStackService.ResultType.NoStack, result);
        }

        [Fact]
        public void Pop_UserExistStackNotEmpty_ReturnMessage()
        {

        }
    }
}
