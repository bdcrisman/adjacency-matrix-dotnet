using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Graph_Fun
{
    class Node
    {
        /// <summary>
        /// Has this node been visited?
        /// </summary>
        public bool IsVisited { get; set; }

        /// <summary>
        /// Edge values.
        /// </summary>
        public List<int> Values { get; set; }
    }
}
