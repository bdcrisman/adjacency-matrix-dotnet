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

        public int Count { get { return _adjacencyMatrix.Count; } }

        private List<Node> _adjacencyMatrix;

        public Graph()
        {
            var valueLists = RetrieveMatrixFromFile(MatrixPath);

            if (valueLists == null || valueLists.Count == 0)
                return;

            _adjacencyMatrix = new List<Node>();

            valueLists.ForEach(
                x => _adjacencyMatrix.Add(new Node(x)));
        }

        public List<int> BFS(int startIndex = 0)
        {
            var l = new List<int>();

            if (_adjacencyMatrix.Count <= 0)
                return l;

            ResetNodesVisitation();
            startIndex = startIndex < 0 ? 0 : startIndex >= _adjacencyMatrix.Count ? _adjacencyMatrix.Count - 1 : startIndex;

            var queue = new Queue<int>();
            queue.Enqueue(startIndex);
            _adjacencyMatrix[startIndex].IsVisited = true;

            BFS(queue, l);

            return l;
        }

        public List<int> DFS(int startIndex = 0)
        {
            var l = new List<int>();

            if (_adjacencyMatrix.Count <= 0)
                return l;

            ResetNodesVisitation();
            startIndex = startIndex < 0 ? 0 : startIndex >= _adjacencyMatrix.Count ? _adjacencyMatrix.Count - 1 : startIndex;

            l.Add(startIndex);
            _adjacencyMatrix[startIndex].IsVisited = true;
            
            DFS(startIndex, 0, l);

            return l;
        }

        public void ShortestPathTo(int dest)
        {
            
        }

        private void DFS(int v, int i, List<int> l)
        {
            if (Math.Max(v, i) >= _adjacencyMatrix.Count)
                return;

            if (!_adjacencyMatrix[i].IsVisited)
            {
                l.Add(i);
                _adjacencyMatrix[i].IsVisited = true;
                DFS(i, 0, l);
            }
            else
                DFS(v, i + 1, l);
        }

        private void BFS(Queue<int> q, List<int> l)
        {
            if (q.Count <= 0)
                return;

            var front = q.Dequeue();
            l.Add(front);

            LoadAdjacencies(q, front, 0);
            BFS(q, l);
        }

        private void LoadAdjacencies(Queue<int> q, int adjIndex, int valIndex)
        {
            if (valIndex >= _adjacencyMatrix[adjIndex].Values.Count)
                return;

            var v = _adjacencyMatrix[adjIndex].Values[valIndex];

            if (!_adjacencyMatrix[v].IsVisited)
            { 
                _adjacencyMatrix[v].IsVisited = true;
                q.Enqueue(v);
            }

            LoadAdjacencies(q, adjIndex, valIndex + 1);
        }

        //private void DFS_Display(Stack<int> s)
        //{
        //    if (s.Count <= 0)
        //        return;

        //    var top = s.Pop();
        //    Console.WriteLine(top);

        //    for (var i = 0; i < _adjacencyMatrix[top].Values.Count; ++i)
        //    {
        //        if (!_adjacencyMatrix[top].IsVisited)
        //        {
        //            _adjacencyMatrix[top].IsVisited = true;
        //            s.Push(_adjacencyMatrix[top].Values[i]);
        //        }
        //        else
        //        {
        //            s.Pop();
        //        }

        //        DFS_Display(s);
        //    }

        //    //LoadDepth(s, top, 0);
        //    //DFS_Display(s);
        //}

        //private void LoadDepth(Stack<int> s, int adjIndex, int valIndex)
        //{
        //    if (valIndex >= _adjacencyMatrix[adjIndex].Values.Count)
        //        return;

        //    var v = _adjacencyMatrix[adjIndex].Values[valIndex];

        //    if (_adjacencyMatrix[v].IsVisited)
        //    {
        //        _adjacencyMatrix[v].IsVisited = true;
        //        s.Push(v);
        //    }
        //    else if (s.Count > 0)
        //    {
        //        s.Pop();
        //    }
            
        //    LoadDepth(s, v, 0);
        //}

        private void ResetNodesVisitation()
        {
            _adjacencyMatrix.ForEach(x => x.IsVisited = false);
        }

        private List<List<int>> RetrieveMatrixFromFile(string path)
        {
            if (!System.IO.File.Exists(path))
                return null;

            return JsonConvert.DeserializeObject<List<List<int>>>(System.IO.File.ReadAllText(path));
        }

        #region Testing
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
