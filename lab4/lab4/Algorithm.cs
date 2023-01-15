using System.Diagnostics;

namespace lab4
{
    internal class Algorithm
    {
        private Graph _graph;
        private List<int> _availableVertices;
        private readonly Graph _initialGraph;
        private readonly int[] _paletteArr;
        private readonly List<int> _usedColorsList;

        public Algorithm(Graph initialGraph)
        {
            _initialGraph = initialGraph;
            _graph = new Graph(initialGraph);
            _availableVertices = Util.GetVertices();
            _paletteArr = new int[Constant.MaxVertexDegree + 1];
            for (int i = 0; i < Constant.MaxVertexDegree + 1; i++)
            {
                _paletteArr[i] = i;
            }

            _usedColorsList = new List<int>();
        }

        public Graph Run()
        {
            Graph resultGraph = new Graph(_graph);
            int bestChromaticNumber = GetBestChromaticNumber();

            Console.WriteLine("Initialize colored graph: ");
            Util.PrintArray(_graph.GetColors());
            Console.WriteLine(
                "The optimal solution for the graph was found on the first iteration, with the previous maximum vertex " +
                $"degree being {Constant.MaxVertexDegree + 1} and the new best chromatic number being {bestChromaticNumber}. " +
                "The estimated time for this process was 0 seconds.");
            Reset();
            
            for (int iterations = 0; iterations < Constant.MaxIterationsCount;)
            {
                Stopwatch sw = Stopwatch.StartNew();
                for (int k = 1; k < Constant.IterationsPerStep + 1; k++, Reset())
                {
                    int newChromaticNumber = GetBestChromaticNumber();
                    if (newChromaticNumber >= bestChromaticNumber) continue;
                    Console.WriteLine($"After {iterations + k} iterations, a new optimal solution for the graph was found. " +
                                      $"The previous best chromatic number was {bestChromaticNumber}, and the new best chromatic number " +
                                      $"is {bestChromaticNumber = newChromaticNumber}. The estimated time for this process was " +
                                      $"{sw.ElapsedMilliseconds / 1000} seconds.");
                    resultGraph = new Graph(_graph);
                }

                Console.WriteLine($"On iteration {iterations += Constant.IterationsPerStep}, the best result found was {bestChromaticNumber}. " +
                                  $"The estimated time for this process was {sw.ElapsedMilliseconds / 1000} seconds.");
            }

            Console.WriteLine("Initial colors of graph are (-1 - no color): ");
            Util.PrintArray(_graph.GetColors());
            return resultGraph;
        }

        private void Reset()
        {
            _usedColorsList.Clear();
            _availableVertices = Util.GetVertices();
            _graph = new Graph(_initialGraph);
        }

        private int GetBestChromaticNumber()
        {
            while (!_graph.IsGraphProperlyColored())
            {
                ColorSelectedVertices(SelectExplorerVertices());
            }

            return _usedColorsList.Count;
        }

        private List<int> SelectExplorerVertices()
        {
            var selectedVertices = new List<int>();
            var random = new Random();
            int numberOfExplorers = Constant.ExplorerBeesCount;
            while (numberOfExplorers > 0 && _availableVertices.Count > 0)
            {
                int index = random.Next(_availableVertices.Count);
                int randomSelectedVertex = _availableVertices[index];
                _availableVertices.RemoveAt(index);
                selectedVertices.Add(randomSelectedVertex);
                numberOfExplorers--;
            }

            return selectedVertices;
        }


        private void ColorSelectedVertices(IReadOnlyList<int> selectedVertices)
        {
            var degrees = new int[selectedVertices.Count];
            for (int i = 0; i < degrees.Length; i++)
            {
                degrees[i] = _graph.GetDegreeOfVertex(selectedVertices[i]);
            }

            var onlookerBeesCounts = Util.GetOnlookersBeesSplit(degrees);
            for (int i = 0; i < selectedVertices.Count; i++)
            {
                var connectedVertices = _graph.GetAdjacentVertices(selectedVertices[i]);
                ColorConnectedVertex(connectedVertices, onlookerBeesCounts[i]);
                ColorVertex(selectedVertices[i]);
            }
        }

        private void ColorConnectedVertex(IReadOnlyList<int> connectedVertices, int onlookerBeesCount)
        {
            for (int i = 0; i < connectedVertices.Count; ++i)
            {
                if (i < onlookerBeesCount - 1) 
                    ColorVertex(connectedVertices[i]);
            }
        }

        private void ColorVertex(int vertex)
        {
            var availableColors = new HashSet<int>(_usedColorsList);
            var random = new Random();
            int color;
            while (true)
            {
                if (availableColors.Count == 0)
                {
                    color = _paletteArr[_usedColorsList.Count];
                    _usedColorsList.Add(color);
                    break;
                }

                color = availableColors.ElementAt(random.Next(availableColors.Count));
                availableColors.Remove(color);
                if (_graph.IsColorChangeValid(vertex, color))
                    break;
            }

            _graph.IsColorChangeValid(vertex, color);
        }
    }
}