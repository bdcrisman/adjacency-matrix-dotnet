using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Graph_Fun
{
    class Node
    {
        public bool IsVisited { get; set; }

        public List<int> Values { get; set; }

        public Node(List<int> values)
        {
            IsVisited = false;
            Values = values;
        }
    }
}
