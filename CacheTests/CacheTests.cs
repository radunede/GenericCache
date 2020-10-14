using System;
using System.Collections.Generic;
using Cache;
using Xunit;

namespace CacheTests
{

    public class CacheTests
    {

 
        [Fact]
        public void Cache_Should_Correctly_Return_Value()
        {
            ICache cache = new Cache.Cache(new DoubleLinkedList());
            cache.Add("A", 1);
            cache.Add("B", 2);
            cache.Add("C", 3);

            Assert.Equal(3, cache.Get("C"));

        }

        [Fact]
        public void Cache_Should_Correctly_Resize()
        {
            ICache cache = new Cache.Cache(new DoubleLinkedList());
            cache.Add("A", 1);
            cache.Add("B", 2);
            cache.Add("C", 3);
            cache.SetCacheSize(2);

            Assert.Null(cache.Get("A"));

        }

        [Fact]
        public void Cache_Should_Correctly_Remove_Last()
        {
            ICache cache = new Cache.Cache(new DoubleLinkedList());
            cache.Add("A", 1);
            cache.Add("B", 2);
            cache.Add("C", 3);
            cache.SetCacheSize(2);
            cache.Add("A", 1);

            Assert.Equal(3, cache.Get("C"));

        }


        [Fact]
        public void Cache_Should_Correctly_Remove_Last_Used()
        {
            ICache cache = new Cache.Cache(new DoubleLinkedList());
            cache.Add("A", 1);
            cache.Add("B", 2);
            cache.Add("C", 3);
            cache.Add("D", 4);
            cache.SetCacheSize(4);

            cache.Get("B");
            cache.Get("C");
            cache.Get("A");
            cache.Add("F", 5);
            

            Assert.Null(cache.Get("D"));
        }

        [Fact]
        public void Cache_Should_Correctly_Remove_Last_Used_And_Invoke_Callback()
        {
            ICache cache = new Cache.Cache(new DoubleLinkedList());
            int i = 0;
            cache.Add("A", 1);
            cache.Add("B", 2);
            cache.Add("C", 3);
            cache.Add("D", 4, () => { i = i + 1; });
            cache.SetCacheSize(4);

            cache.Get("B");
            cache.Get("C");
            cache.Get("A");
            cache.Add("F", 5);

            Assert.Null(cache.Get("D"));
            Assert.Equal(1, i);
        }

        [Fact]
        public void Cache_Should_Correctly_Store_Any_Object()
        {
            ICache cache = new Cache.Cache(new DoubleLinkedList());
            cache.Add("A", "RandomString");
            cache.Add("B", 20m);
            cache.Add("C", new Node("Test"));
            cache.Add("D", new List<string>() { "A", "B"});
            cache.SetCacheSize(4);

            cache.Get("B");
            cache.Get("C");
            cache.Get("A");
            cache.Add("F", 5);


            Assert.Null(cache.Get("D"));
        }


    }
}
