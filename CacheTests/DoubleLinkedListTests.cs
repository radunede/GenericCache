using System;
using Cache;
using Xunit;

namespace CacheTests
{
    public class DoubleLinkedListTests
    {
        [Fact]
        public void Node_Removed_Should_Be_First_Inserted()
        {
            DoubleLinkedList doubleLinkedList = new DoubleLinkedList();
            doubleLinkedList.Insert("A");
            doubleLinkedList.Insert("B");
            doubleLinkedList.Insert("C");

            Assert.Equal("A", doubleLinkedList.Remove());

        }

        [Fact]
        public void First_Node_Should_Be_Last_Inserted()
        {
            DoubleLinkedList doubleLinkedList = new DoubleLinkedList();
            doubleLinkedList.Insert("A");
            doubleLinkedList.Insert("B");
            doubleLinkedList.Insert("C");

            Assert.Equal("C", doubleLinkedList.GetMostUsed());

        }
    }
}
