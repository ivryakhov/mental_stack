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
        private const string _user = "TestUser";
        private const string _message = "Test message";

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
            var result = mStackService.Push(_user, _message);
            object expectedValue = null;

            _memoryCacheMock
                .Setup(m => m.TryGetValue(It.IsAny<string>(), out expectedValue))
                .Returns(false);

            Assert.Equal(MStackService.ResultType.UserAdded, result);
            _memoryCacheMock.Verify(x => x.CreateEntry(_user));
        }

        [Fact]
        public void Push_UserAlreadyExists_UpdateMStackForTheUser()
        {
            var messages = new Stack<string>();
            messages.Push(_message);
            var mStack = new MStack { User = _user, Messages = messages };
            object expectedValue = mStack;   

            _memoryCacheMock
                .Setup(m => m.TryGetValue(It.IsAny<string>(), out expectedValue))
                .Returns(true);

            var mStackService = new MStackService(_memoryCacheMock.Object);
            var result = mStackService.Push(_user, _message);

            Assert.Equal(MStackService.ResultType.UserUpdated, result);
            _memoryCacheMock.Verify(x => x.CreateEntry(_user));
        }

        [Fact]
        public void Pop_UserDoesNotExist_ReturnNoStackResult()
        {
            object expectedValue = null;

            _memoryCacheMock
                .Setup(m => m.TryGetValue(It.IsAny<string>(), out expectedValue))
                .Returns(false);

            var mStackService = new MStackService(_memoryCacheMock.Object);
            var result = mStackService.Pop(_user, out string expectedMessage);

            Assert.Equal(MStackService.ResultType.NoStack, result);
            Assert.Equal("", expectedMessage);
        }

        [Fact]
        public void Pop_UserExistsStackNotEmpty_ReturnAppropriateMessage()
        {
            var messages = new Stack<string>();
            messages.Push(_message);
            object expectedValue = new MStack { User = _user, Messages = messages };

            _memoryCacheMock
                .Setup(m => m.TryGetValue(It.IsAny<string>(), out expectedValue))
                .Returns(true);

            var mStackService = new MStackService(_memoryCacheMock.Object);
            var result = mStackService.Pop(_user, out string expectedMessage);

            Assert.Equal(MStackService.ResultType.PopSuccess, result);
            Assert.Equal(_message, expectedMessage);
        }

        [Fact]
        public void Pop_UserExistsStackIsEmpty_ReturnEmptyStackResult()
        {
            var messages = new Stack<string>();
            object expectedValue = new MStack { User = _user, Messages = messages };

            _memoryCacheMock
                .Setup(m => m.TryGetValue(It.IsAny<string>(), out expectedValue))
                .Returns(true);

            var mStackService = new MStackService(_memoryCacheMock.Object);
            var result = mStackService.Pop(_user, out string expectedMessage);

            Assert.Equal(MStackService.ResultType.EmptyStack, result);
            Assert.Null(expectedMessage);
        }
    }
}
