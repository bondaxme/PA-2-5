namespace lab1;

public class Funcs
{

    public static void GenerateFile()
    {
        Console.Write("Enter the size of file (MB): ");
        int mb = Convert.ToInt32(Console.ReadLine());
        long bytes = mb * 1024 * 1024;
        using (BinaryWriter file = new BinaryWriter(File.Open("A1.dat", FileMode.Create)))
        {
            for (int i = 0; i < bytes/4; i++)
            {
                Random rand = new Random();
                int rand_num = rand.Next(150000);
                file.Write(rand_num);
            }
        }
    }
    
    public static void CreateFiles(int number_of_files)
    {
        for (int i = 1; i <= number_of_files; i++)
        {
            BinaryWriter files_C = new BinaryWriter(File.Open($"C{i}.dat", FileMode.Create));
            BinaryWriter files_B = new BinaryWriter(File.Open($"B{i}.dat", FileMode.Create));
            files_B.Close();
            files_C.Close();
        }
    }
    
    public static List<string> CreateFilesLists(int number_of_files, char name)
    {
        List<string> files = new List<string>();
        for (int i = 1; i <= number_of_files; i++)
            files.Add($"{name}{i}.dat");
        return files;
    }

    public static void PrintFile(string path)
    {
        BinaryReader br = new BinaryReader(File.Open(path, FileMode.Open));
        
        Console.Write("[");
        for (int i = 0; i < 50; i++)
            Console.Write($" {BitConverter.ToInt32(br.ReadBytes(4), 0)} ");
        Console.Write(".....");

        long size = new FileInfo(path).Length;
        br.BaseStream.Seek(size - 200, SeekOrigin.Begin);

        for (int i = 0; i < 50; i++)
            Console.Write($" {BitConverter.ToInt32(br.ReadBytes(4), 0)} ");
        Console.WriteLine("]");
    }
}