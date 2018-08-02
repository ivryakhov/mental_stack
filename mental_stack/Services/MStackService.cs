using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mental_stack.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace mental_stack.Services
{
    public class MStackService
    {
        private const int _timeout = 5;
        //private MemoryContext _db;
        private IMemoryCache _cache;
        public MStackService(IMemoryCache memoryCache)
        {
           // _db = context;
            _cache = memoryCache;
        }

        public void AddNewMStack(string user, string message)
        {
            Stack<string> messages = new Stack<string>();
            messages.Push(message);
            MStack mStack = new MStack { User = user, Messages = messages };
            //_db.MStacks.Add(mStack);
            SaveChanges(user, mStack);
        }

        public string Push(string user, string message)
        {
            MStack mStack = FetchDatabase(user);
            if (mStack == null)
            {
                AddNewMStack(user, message);                
            }
            else
            {
                mStack.Messages.Push(message);
                SaveChanges(user, mStack);
                //_db.MStacks.Update(mStack);
            }
            
            return "OK";
        }

        public string Pop(string user)
        {
            MStack mStack = FetchDatabase(user);
            if (mStack == null)
            {
                return "NO_SUCH_STACK";
            }
            else
            {
                string message;
                if (mStack.Messages.TryPop(out message))
                {
                   // _db.MStacks.Update(mStack);
                    SaveChanges(user, mStack);
                    return message;
                }
                else
                    return "FAILED_TO_POP_OUT";
            }
        }

        private MStack FetchDatabase(string user)
        {
            MStack mStack = null;
            _cache.TryGetValue(user, out mStack);
            
               // mStack = await _db.MStacks.FirstOrDefaultAsync(m => m.User == user);
            

            return mStack;
        }

        private void SaveChanges(string user, MStack mStack)
        {
           // int n = _db.SaveChanges();
           // if (n > 0)
           // {
                _cache.Set(user, mStack, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_timeout)
                });
           // }
        }
    }
}
