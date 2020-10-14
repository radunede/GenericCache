using System;
namespace Cache
{
    public interface IDoubleLinkedList
    {

        string Remove(string key = null);
        void Insert(string key, Action callback = null);
        string GetMostUsed();
        string GetLeastUsed();
    }
}
