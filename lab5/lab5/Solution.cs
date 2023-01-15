namespace lab5;

public struct Solution
{
    public int[] Path { get; }
    private Problem Problem { get; }
    public int Cost { get; }

    public Solution(Problem problem, int[] path)
    {
        Path = path;
        Problem = problem;
        Cost = Problem.GetCost(path);
    }
}