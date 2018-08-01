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
        private MemoryContext db;
        private IMemoryCache cache;
        public MStackService(MemoryContext context, IMemoryCache memoryCache)
        {
            db = context;
            cache = memoryCache;
        }

        public void AddNewMStack(string user, string message)
        {
            Stack<string> newStack = new Stack<string>;
            newStack.Push(message);
            MStack mStack = new MStack { User = user, Messages = newStack };
            db.MStacks.Add(mStack);
            int n = db.SaveChanges();
            if (n > 0)
            {
                cache.Set(user, mStack, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
            }
        }

        public async Task<string> Push(string user, string message)
        {
            MStack mStack = await db.MStacks.FirstOrDefaultAsync(m => m.User == user);
            if (mStack == null)
            {
                AddNewMStack(user, message);
                
            }
            else
            {
                mStack.Messages.Push(message);
            }
            return "OK";
        }
    }
}
