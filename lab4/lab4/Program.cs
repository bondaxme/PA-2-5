using System.Diagnostics;

namespace lab4;

class Program
{
    public static void Main(string[] args)
    {
        var adjMatrix = new int[Constant.NumberOfVertices, Constant.NumberOfVertices];
        var graph = new Graph(adjMatrix);

        Console.WriteLine("Matrix is valid: " + graph.IsMatrixValid());

        Util.PrintMatrix(graph.AdjacencyMatrix);
        Console.WriteLine("Graph degrees: ");
        Util.PrintArray(graph.GetVertexDegrees());

        Console.WriteLine("Training is started successfully, stand by...");
        var sw = Stopwatch.StartNew();

        graph = new Algorithm(graph).Run();
        sw.Stop();
        Console.WriteLine($"Time to train: {sw.ElapsedMilliseconds / 1000}s");
        
        Console.WriteLine("Graph coloring: ");
        Util.PrintArray(graph.GetColors());

        Console.WriteLine("Graph colored properly: " + graph.IsGraphProperlyColored());
    }
}
