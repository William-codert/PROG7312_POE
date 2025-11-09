namespace PROG7312_POE.Models.DataStructures
{
    public class Graph
    {
        private Dictionary<string, List<string>> _adjacencyList = new();

        public void AddEdge(string from, string to)
        {
            if (!_adjacencyList.ContainsKey(from))
                _adjacencyList[from] = new List<string>();
            _adjacencyList[from].Add(to);
        }

        public List<string> BFS(string start)
        {
            List<string> visited = new();
            Queue<string> queue = new();
            queue.Enqueue(start);
            visited.Add(start);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (!_adjacencyList.ContainsKey(node)) continue;
                foreach (var neighbor in _adjacencyList[node])
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }
            return visited;
        }
    }
}
