using System;
using System.Threading.Tasks;
using Cache;
using Xunit;

namespace CacheTests
{
    public class MultithreadedCacheTests
    {
        [Fact]
        public void Multiple_Threads_Should_Correctly_Insert()
        {
            ICache cache = new Cache.Cache(new DoubleLinkedList());
            // not ideal as Invoke does not guarantee threads will run at the same time.
            Parallel.Invoke(
                () => {
         
                    cache.Add("A", 1);
                },
                () => {
                    cache.Add("B", 2);
                },
                () => {
                    cache.Add("C", 3);
                },
                () => {
                    cache.Add("D", 4);
                },
                () => {
                    cache.Add("E", 5);
                },
                () => {
                    cache.Add("F", 6);
                },
                () => {
                
                    cache.Get("A");
                    cache.Get("D");
                },
                () => {
              
                    cache.Get("B");
                    cache.Get("E");
                },
                () => {
                   
                    cache.Get("C");
                },
                () => {
                    
                    cache.SetCacheSize(5);
                });
            Assert.Equal(5, cache.Count);

        }
    }
}
