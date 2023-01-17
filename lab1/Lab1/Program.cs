using static lab1.Funcs;
using static lab1.Algorithm;

namespace lab1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter the amount of files to create for dividing: ");
            int amount_of_files = Convert.ToInt32(Console.ReadLine());
            string sorted_filename = BalancedKwayMerge(amount_of_files);
            List<int> numbers = new List<int>() {27, 4, 8, 55, 11, };
            Console.Write("The initial file: ");
            PrintFile("A1.dat");
            Console.Write($"The sorted file {sorted_filename}: ");
            PrintFile(sorted_filename);
        }
    }
}