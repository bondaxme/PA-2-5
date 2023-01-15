namespace lab5;

public class Algorithm
{
    private readonly Problem _problem;

    private Solution[] CurrentGeneration { get; set; }

    public Algorithm(Problem problem)
    {
        _problem = problem;
        CurrentGeneration = Util.GetInitialPopulation(problem);
    }

    public Solution GetBestSolution()
    {
        return CurrentGeneration.MinBy(s => s.Cost);
    }

    public void EvolveGeneration()
    {
        var breedingPool = CurrentGeneration.OrderBy(s => s.Cost).Take(Constant.ElitePoolSize).ToArray();
        var best = breedingPool.Take(Constant.ElitePoolSize / 5).ToArray();

        var probabilities = Util.GetProbabilities(breedingPool);

        var newPopulation = new Solution[Constant.TotalCities];

        for (int i = 0; i < Constant.ElitePoolSize / 5; i++)
        {
            newPopulation[i] = best[i];
        }

        Random random = new Random();
        for (int i = Constant.ElitePoolSize / 5; i < Constant.TotalCities; i++)
        {
            newPopulation[i] = CreateOffspring(breedingPool[Util.ChooseSolution(probabilities)],
                breedingPool[Util.ChooseSolution(probabilities)]);
            if (random.NextDouble() < Constant.Probability)
            {
                Util.Mutate(newPopulation[i]);
            }
        }

        CurrentGeneration = newPopulation;
    }

    private Solution CreateOffspring(Solution first, Solution second)
    {
        var firstHalfGenes = ExtractFirstHalfGenes(first);
        var path = CombinePaths(first, second, firstHalfGenes);
        return new Solution(_problem, path);
    }

    private int[] ExtractFirstHalfGenes(Solution first)
    {
        var firstHalfGenes = new int[Constant.TotalCities / 2];
        for (int i = 0; i < Constant.TotalCities / 2; i++)
        {
            firstHalfGenes[i] = first.Path[Constant.BatchSize * 2 * (i / Constant.BatchSize) + i % Constant.BatchSize];
        }
        return firstHalfGenes;
    }

    private int[] CombinePaths(Solution first, Solution second, int[] firstHalfGenes)
    {
        var path = new int[Constant.TotalCities];
        var currentSecond = 0;
        for (int i = 0; i < Constant.TotalCities; i++)
        {
            if ((i / Constant.BatchSize) % 2 == 0)
            {
                path[i] = first.Path[i];
            }
            else
            {
                while (firstHalfGenes.Contains(second.Path[currentSecond]))
                {
                    currentSecond++;
                }

                path[i] = second.Path[currentSecond++];
            }
        }
        return path;
    }

    public void RunIterations(int iterationCount)
    {
        for (int j = 0; j < iterationCount; j++)
        {
            EvolveGeneration();
        }
    }
}