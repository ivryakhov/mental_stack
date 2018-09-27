using System;
using System.Collections.Generic;
using MentalStack.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace MentalStack.Services
{
    public class MStackService
    {
        public enum ResultType { UserAdded, UserUpdated, PopSuccess, EmptyStack, NoStack, };
        private const int _timeout = 5;
        private IMemoryCache _cache;
        public MStackService(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public ResultType Push(string user, string message)
        {
            ResultType result;
            MStack mStack = null;
            if(!_cache.TryGetValue(user, out mStack))
            {
                mStack = CreateNewMStack(user, message);
                result = ResultType.UserAdded;
            }
            else
            {
                mStack.Messages.Push(message);
                result = ResultType.UserUpdated;
                
            }
            SaveChanges(user, mStack);
            return result;
        }

        public ResultType Pop(string user, out string message)
        {
            message = "";
            MStack mStack = null;
            if (_cache.TryGetValue(user, out mStack))
            {
                if (mStack.Messages.TryPop(out message))
                {
                    SaveChanges(user, mStack);
                    return ResultType.PopSuccess;
                }
                else
                    return ResultType.EmptyStack;
            }
            else
            {
                return ResultType.NoStack; 
            }
        }

        private MStack CreateNewMStack(string user, string message)
        {
            Stack<string> messages = new Stack<string>();
            messages.Push(message);
            return new MStack { User = user, Messages = messages };
         }

        private void SaveChanges(string user, MStack mStack)
        {
            _cache.Set(user, mStack, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_timeout)
                });
        }
    }
}
