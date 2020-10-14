using System;
namespace Cache
{
    public class Node
    {
        public Node(string value)
        {
            Value = value;
        }

        public string Value { get; }
        public Node Next { get; set; }
        public Node Previous { get; set; }
        public Action Callback { get; internal set; }
    }
}
