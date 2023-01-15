namespace lab5;

public abstract class Constant
{
    public const int MaxPopulation = 1000;
    public const int ElitePoolSize = 100;
    public const string DataFile = "problem.json";
    public const int TotalCities = 300;
    private const int BatchNumber = 4;
    public const int BatchSize = TotalCities / BatchNumber;
    public const double Probability = 0.3;
}