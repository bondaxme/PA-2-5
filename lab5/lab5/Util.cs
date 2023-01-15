using System.Security.Cryptography;

namespace lab5;

public class Util
{
    public static double[] GetProbabilities(Solution[] solutions)
    {
        var values = new double[solutions.Length];
        var sum = 0.0;
        for (int i = 0; i < solutions.Length; i++)
        {
            values[i] = 1.0 / solutions[i].Cost;
            sum += values[i];
        }

        for (int i = 0; i < values.Length; i++)
        {
            values[i] /= sum;
        }

        return values;
    }
    
    public static Solution[] GetInitialPopulation(Problem problem)
    {
        Random rand = new Random();
        var population = new Solution[Constant.MaxPopulation];
        var random = Enumerable.Range(0, Constant.TotalCities).OrderBy(_ => rand.Next()).ToArray();
        for (int i = 0; i < Constant.MaxPopulation; i++)
        {
            population[i] = new Solution(problem, random);
        }
        return population;
    }
    
    public static int ChooseSolution(IReadOnlyList<double> probabilities)
    {
        Random rand = new Random();
        var random = rand.NextDouble();
        var sum = 0.0;
        for (int i = 0; i < probabilities.Count; i++)
        {
            sum += probabilities[i];
            if (sum > random)
            {
                return i;
            }
        }

        return probabilities.Count - 1;
    }
    
    public static int[][] GenerateMatrix()
    {
        int[][] matrix = new int[Constant.TotalCities][];
        for (int i = 0; i < Constant.TotalCities; i++)
        {
            matrix[i] = new int[Constant.TotalCities];
            for (int j = 0; j < Constant.TotalCities; j++)
            {
                if (j > i)
                {
                    matrix[i][j] = RandomNumberGenerator.GetInt32(5, 150);
                }
                else if (j == i)
                {
                    matrix[i][j] = int.MaxValue;
                }
                else
                {
                    matrix[i][j] = matrix[j][i];
                }
            }
        }

        return matrix;
    }
    
    public static void Mutate(Solution solution)
    {
        Random random = new Random();
        var first = random.Next(0, solution.Path.Length);
        var second = random.Next(0, solution.Path.Length);
        (solution.Path[first], solution.Path[second]) = (solution.Path[second], solution.Path[first]);
    }
}