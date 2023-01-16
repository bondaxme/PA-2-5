using System.Text;

namespace lab5
{
    class Program
    {
        public static void Main(string[] args)
        {
            var result = new StringBuilder();
            var problem = new Problem();
            var algorithm = new Algorithm(problem);
            for (int i = 0; i < 100; i++)
            {
                algorithm.RunIterations(100);
                var bestSolution = algorithm.GetBestSolution();
                var line = $"iteration count: {i * 100 + 100}, cost on this iteration: {bestSolution.Cost}";
                result.AppendLine(line);
                Console.WriteLine(line);
            }
        }
    }
}