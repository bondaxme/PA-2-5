using System.Security.Cryptography;
using System.Text.Json;

namespace lab5;

public class Problem
{
    private int[][] Matrix { get; set; }

    public Problem()
    {
        InitializeMatrix();
    }

    public int GetCost(int[] path)
    {
        var solution = 0;
        for (int i = 0; i < Constant.TotalCities - 1; i++)
        {
            solution += GetDistance(path[i], path[i + 1]);
        }

        solution += GetDistance(path[Constant.TotalCities - 1], path[0]);

        return solution;
    }

    private int GetDistance(int source, int destination) => Matrix[source][destination];
    
    private void InitializeMatrix()
    {
        if (MatrixFileExists())
        {
            Matrix = ReadMatrixFromFile();
            return;
        }

        Matrix = GenerateMatrix();
        SaveMatrixToFile();
    }

    private bool MatrixFileExists()
    {
        return File.Exists(Constant.DataFile);
    }

    private int[][] ReadMatrixFromFile()
    {
        using var fs = new FileStream(Constant.DataFile, FileMode.Open);
        return JsonSerializer.Deserialize<int[][]>(fs)!;
    }

    private int[][] GenerateMatrix()
    {
        return Util.GenerateMatrix();
    }

    private void SaveMatrixToFile()
    {
        using var fs = new FileStream(Constant.DataFile, FileMode.Create);
        JsonSerializer.Serialize(fs, Matrix);
    }
}