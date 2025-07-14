using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Utils
{
    public class Locker
    {
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        public async Task<TResult> Run<TResult>(Func<Task<TResult>> cb)
        {
            await _semaphore.WaitAsync();
            try
            {
                return await cb.Invoke();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task Run(Func<Task> cb)
        {
            await _semaphore.WaitAsync();
            try
            {
                await cb.Invoke();
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
