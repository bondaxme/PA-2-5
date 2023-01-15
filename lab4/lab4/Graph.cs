namespace lab4
{
    internal class Graph
    {
        private readonly int[,] _adjacencyMatrix;
        private readonly int[] _colors;

        public Graph(Graph g)
        {
            _adjacencyMatrix = new int[g._adjacencyMatrix.GetLength(0), g._adjacencyMatrix.GetLength(1)];
            _colors = new int[g._colors.Length];

            Array.Copy(g._adjacencyMatrix, _adjacencyMatrix, g._adjacencyMatrix.Length);
            Array.Copy(g._colors, _colors, g._colors.Length);
        }

        public Graph(int[,] adjacencyMatrix)
        {
            Random rand = new Random();
            _adjacencyMatrix = adjacencyMatrix;
            _colors = new int[adjacencyMatrix.GetLength(0)];
            Array.Fill(_colors, Constant.NoColor);
        
            for (int currentVertex = 0; currentVertex < Constant.VertexCount; ++currentVertex)
            {
                int finalVertexDegree = Math.Min(rand.Next(Constant.MinVertexDegree, Constant.MaxVertexDegree + 1)
                                                 - GetDegreeOfVertex(currentVertex), Constant.VertexCount - currentVertex - 1);
                for (int newConnections = 0; newConnections < finalVertexDegree; ++newConnections)
                {
                    bool isConnectedAlready = true;
                    for (int tryCount = 0, newVertex = rand.Next(currentVertex + 1, Constant.VertexCount);
                         isConnectedAlready && tryCount < Constant.VertexCount;
                         ++tryCount, newVertex = rand.Next(currentVertex + 1, Constant.VertexCount))
                    {
                        if (_adjacencyMatrix[currentVertex, newVertex] == 0 &&
                            GetDegreeOfVertex(newVertex) < Constant.MaxVertexDegree)
                        {
                            isConnectedAlready = false;
                            _adjacencyMatrix[currentVertex, newVertex] = 1;
                            _adjacencyMatrix[newVertex, currentVertex] = 1;
                        }
                    }
                }
            }
        }

        public bool IsMatrixValid()
        {
            for (int vertex = 0; vertex < _adjacencyMatrix.GetLength(0); vertex++)
            {
                if (GetDegreeOfVertex(vertex) > Constant.MaxVertexDegree) 
                    return false;
            }

            return true;
        }


        public bool IsGraphProperlyColored()
        {
            for (int i = 0; i < _colors.Length; i++)
            {
                if (_colors[i] == Constant.NoColor) 
                    return false;
            }

            return IsColoringValid();
        }

        public int[] GetColors()
        {
            return _colors;
        }

        public int[] GetVertexDegrees()
        {
            int[] vertexDegrees = new int[_adjacencyMatrix.GetLength(0)];
            for (int i = 0; i < vertexDegrees.Length; ++i)
            {
                vertexDegrees[i] = GetDegreeOfVertex(i);
            }

            return vertexDegrees;
        }

        public int GetDegreeOfVertex(int vertex)
        {
            int degree = 0;
            for (int i = 0; i < _adjacencyMatrix.GetLength(0); i++)
            {
                degree += _adjacencyMatrix[vertex, i];
            }

            return degree;
        }

        public int[] GetAdjacentVertices(int vertex)
        {
            int[] adjacentVertices = new int[GetDegreeOfVertex(vertex)];
            int index = 0;
            for (int i = 0; i < _adjacencyMatrix.GetLength(0); ++i)
            {
                if (_adjacencyMatrix[vertex, i] == 1) 
                    adjacentVertices[index++] = i;
            }

            return adjacentVertices;
        }


        public bool IsColorChangeValid(int vertex, int newColor)
        {
            int previousColor = _colors[vertex];
            _colors[vertex] = newColor;
            bool isValid = IsColoringValid();
            if (!isValid) 
                _colors[vertex] = previousColor;

            return isValid;
        }


        private bool IsColoringValid()
        {
            for (int i = 0; i < _adjacencyMatrix.GetLength(0); i++)
            {
                for (int j = i + 1; j < _adjacencyMatrix.GetLength(1); j++)
                {
                    if (_adjacencyMatrix[i, j] == 1 && _colors[i] != Constant.NoColor && _colors[i] == _colors[j]) 
                        return false;
                }
            }

            return true;
        }

        public int[,] AdjacencyMatrix => _adjacencyMatrix;
    }
}