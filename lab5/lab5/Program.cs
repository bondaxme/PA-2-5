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
            for (int i = 0; i < 500; i++)
            {
                algorithm.RunIterations(20);
                var bestSolution = algorithm.GetBestSolution();
                var line = $"Iteration: {i * 20 + 20}, Cost: {bestSolution.Cost}";
                result.AppendLine(line);
                Console.WriteLine(line);
            }
            WriteResultToFile("result.txt", result.ToString());
        }
        
        private static void WriteResultToFile(string fileName, string content)
        {
            File.WriteAllText(fileName, content);
        }

    }
}