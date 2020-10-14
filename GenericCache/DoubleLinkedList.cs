using System;
namespace Cache
{
    public class DoubleLinkedList : IDoubleLinkedList
    {

        public DoubleLinkedList(int size = 100)
        {
            Limit = size;
            First = null;
            Last = null;
        }

        private int Limit { get; }
        private int Count { get; set; }
        private Node First { get; set; }
        private Node Last { get; set; }

        public void Insert(string key, Action callback = null)
        {
            Node node = new Node(key);
            node.Previous = null;
            node.Next = First;
            if (callback != null)
                node.Callback = callback;

            if (First != null)
                First.Previous = node;
            else
                Last = node;
            First = node;
            Count++;
        }
        public string Remove(string key = null)
        {
            if (First != null && First.Value == key)
            {
                var temp = First;
                First = First.Next;
                First.Previous = null;
                temp.Callback?.Invoke();                
                string keyReturned = temp.Value;
                temp = null;
                Count--;
                return keyReturned;
            }

            if (Last != null && Last.Value == key || key == null)
            {
                var temp = Last;
                Last = Last.Previous;
                Last.Next = null;
                temp.Callback?.Invoke();
                string keyReturned = temp.Value;
                temp = null;
                Count--;
                return keyReturned;
            }

            Node trav;
            trav = First;
            while(trav.Value != key && trav.Next != null)
            {
                trav = trav.Next;
                if (trav.Value == key)
                {
                    trav.Previous.Next = trav.Next;
                    trav.Next.Previous = trav.Previous;
                    string keyReturned = trav.Value;
                    trav.Callback?.Invoke();
               
                    trav = null;
                    Count--;
                    return keyReturned;
                }
            }
            //key does not exist
            return String.Empty;
        }
        public string GetMostUsed()
        {
            if (First != null)
                return First.Value;
            return String.Empty;
        }
        public string GetLeastUsed()
        {
            if (Last != null)
                return Last.Value;
            return String.Empty;
        }
    }
}
