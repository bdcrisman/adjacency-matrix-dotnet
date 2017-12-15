using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Graph_Fun
{
    class Graph
    {
        const string MatrixPath = "BIN/matrix.json";

        public int Count { get { return _adjacencyMatrix != null ? _adjacencyMatrix.Count : 0; } }

        private List<Node> _adjacencyMatrix;

        /// <summary>
        /// Creates a new graph that loads from a file.
        /// </summary>
        public Graph(bool loadFromFile)
        {
            var valueLists = RetrieveMatrixFromFile(MatrixPath);

            if (valueLists == null || valueLists.Count == 0)
                return;

            _adjacencyMatrix = new List<Node>();

            valueLists.ForEach(
                x => _adjacencyMatrix.Add(new Node() { IsVisited = false, Values = x }));
        }

        /// <summary>
        /// Determines the shortest path in a BF search.
        /// </summary>
        /// <param name="target">Target value.</param>
        /// <returns>Shortest path list of integers.</returns>
        public List<int> ShortestPathBFS(int target)
        {
            var l = new List<int>();
            for (var i = 0; i < _adjacencyMatrix.Count; ++i)
            {
                var path = BFS(i, target);

                if (path.Count > 0 && (l.Count == 0 || path.Count < l.Count))
                    l = path;
            }
            return l;
        }

        /// <summary>
        /// Determines the shortest path in a DF search.
        /// </summary>
        /// <param name="target">Target value.</param>
        /// <returns>Shortest path list of integers.</returns>
        public List<int> ShortestPathDFS(int target)
        {
            var l = new List<int>();
            for (var i = 0; i < _adjacencyMatrix.Count; ++i)
            {
                var path = DFS(i, target);
                if (path.Count > 0 && (l.Count == 0 || path.Count < l.Count))
                    l = path;
            }
            return l;
        }

        /// <summary>
        /// Breadth first search.
        /// </summary>
        /// <param name="startIndex">Starting vertex index.</param>
        /// <param name="target">Target value.</param>
        /// <returns>List of integers.</returns>
        public List<int> BFS(int startIndex, int target)
        {
            var l = new List<int>();

            if (_adjacencyMatrix.Count <= 0)
                return l;

            ResetNodesVisitation();
            startIndex = startIndex < 0 ? 0 : startIndex >= _adjacencyMatrix.Count ? _adjacencyMatrix.Count - 1 : startIndex;

            var queue = new Queue<int>();
            queue.Enqueue(startIndex);
            _adjacencyMatrix[startIndex].IsVisited = true;

            BFS(queue, l, target);

            return l;
        }

        /// <summary>
        /// Depth first search.
        /// </summary>
        /// <param name="startIndex">Starting vertext index.</param>
        /// <param name="target">Target value.</param>
        /// <returns>List of integers.</returns>
        public List<int> DFS(int startIndex, int target)
        {
            var l = new List<int>();

            if (_adjacencyMatrix.Count <= 0)
                return l;

            ResetNodesVisitation();
            startIndex = startIndex < 0 ? 0 : startIndex >= _adjacencyMatrix.Count ? _adjacencyMatrix.Count - 1 : startIndex;

            //l.Add(startIndex);
            //_adjacencyMatrix[startIndex].IsVisited = true;
            //DFS(startIndex, 0, l);

            DFS(startIndex, l, target);
            return l;
        }

        /// <summary>
        /// Depth first search helper.
        /// </summary>
        /// <param name="v">Vertex index.</param>
        /// <param name="l">List containing path.</param>
        /// <param name="target">Target value.</param>
        private void DFS(int v, List<int> l, int target)
        {
            if (v == target)
                return;

            l.Add(v);
            _adjacencyMatrix[v].IsVisited = true;

            for (var i = 0; i < _adjacencyMatrix[v].Values.Count; ++i)
            {
                var val = _adjacencyMatrix[v].Values[i];
                if (!_adjacencyMatrix[val].IsVisited)
                    DFS(val, l, target);
            }
        }

        //private void DFS(int v, int i, List<int> l)
        //{
        //    if (Math.Max(v, i) >= _adjacencyMatrix.Count)
        //        return;

        //    if (!_adjacencyMatrix[i].IsVisited)
        //    {
        //        l.Add(i);
        //        _adjacencyMatrix[i].IsVisited = true;
        //        DFS(i, 0, l);
        //    }
        //    else
        //        DFS(v, i + 1, l);
        //}

        /// <summary>
        /// Breadth first search helper.
        /// </summary>
        /// <param name="q">Queue to hold order.</param>
        /// <param name="l">List containing total path.</param>
        /// <param name="target">Target value.</param>
        private void BFS(Queue<int> q, List<int> l, int target)
        {
            if (q.Count <= 0)
                return;

            var front = q.Dequeue();

            if (front == target)
                return;

            l.Add(front);

            LoadVertexEdgeValues(q, front, 0);
            BFS(q, l, target);
        }

        /// <summary>
        /// Load the vertex adjacencies.
        /// </summary>
        /// <param name="q">Queue to hold order.</param>
        /// <param name="adjIndex">Current adjaceny matrix index.</param>
        /// <param name="valIndex">Current node value index.</param>
        private void LoadVertexEdgeValues(Queue<int> q, int adjIndex, int valIndex)
        {
            if (valIndex >= _adjacencyMatrix[adjIndex].Values.Count)
                return;

            var v = _adjacencyMatrix[adjIndex].Values[valIndex];

            if (!_adjacencyMatrix[v].IsVisited)
            {
                _adjacencyMatrix[v].IsVisited = true;
                q.Enqueue(v);
            }

            LoadVertexEdgeValues(q, adjIndex, valIndex + 1);
        }

        /// <summary>
        /// Resets the graph's visitation flags.
        /// </summary>
        private void ResetNodesVisitation()
        {
            _adjacencyMatrix.ForEach(x => x.IsVisited = false);
        }

        /// <summary>
        /// Retrieves the adjacency matrix from file.
        /// </summary>
        /// <param name="path">Path to matrix.</param>
        /// <returns>List of lists of integers.</returns>
        private List<List<int>> RetrieveMatrixFromFile(string path)
        {
            if (!System.IO.File.Exists(path))
                return null;

            return JsonConvert.DeserializeObject<List<List<int>>>(System.IO.File.ReadAllText(path));
        }

        #region Testing
        /// <summary>
        /// Displays the complete matrix.
        /// </summary>
        public void DisplayMatrix()
        {
            var index = 0;
            foreach (var row in _adjacencyMatrix)
            {
                Console.Write(String.Format("{0} => ", index++));
                row.Values.ForEach(x => Console.Write(String.Format("{0} -> ", x)));
                Console.Write("null");
                Console.WriteLine();
            }
        }
        #endregion Testing

    }
}
