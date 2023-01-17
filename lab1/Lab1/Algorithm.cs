using static lab1.Funcs;

namespace lab1;

public class Algorithm
{
    public static string BalancedKwayMerge(int files_amount)
    {
        List<string> A_files_list = new List<string>();
        A_files_list.Add("A1.dat");
        List<string> B_files_list = CreateFilesLists(files_amount, 'B');
        List<string> C_files_list = CreateFilesLists(files_amount, 'C');
        GenerateFile();
        CreateFiles(files_amount);
        
        MultiwayMerge(A_files_list, B_files_list, files_amount);

        int flag = 1;
        while (!isSorted(A_files_list, B_files_list, C_files_list))
        {
            if (flag == 1)
                MultiwayMerge(B_files_list, C_files_list, files_amount);
            else
                MultiwayMerge(C_files_list, B_files_list, files_amount);
            flag = -flag;
        }
        
        var A_size = new FileInfo(A_files_list[0]).Length;
        var B_size = new FileInfo(B_files_list[0]).Length;
        if (A_size == B_size)
            return B_files_list[0];
        return C_files_list[0];
    }

    private static Boolean isSorted(List<string> A_files_list, List<string> B_files_list, List<string> C_files_list)
    {
        var A_size = new FileInfo(A_files_list[0]).Length;
        var B_size = new FileInfo(B_files_list[0]).Length;
        var C_size = new FileInfo(C_files_list[0]).Length;
        return A_size == B_size || A_size == C_size;
    }
    
    private static void MultiwayMerge(List<string> files_to_divide, List<string> divided_files, int amount_of_files)
    {
        List<int> sequence = new List<int>();

        List<Reader> readers_list = new List<Reader>();
        foreach (var file in files_to_divide)
        {
            var reader = new Reader(file);
            readers_list.Add(reader);
        }

        List<BinaryWriter> writers_list = new List<BinaryWriter>();
        foreach (var file in divided_files)
        {
            BinaryWriter writer = new BinaryWriter(File.Open(file, FileMode.Create));
            writers_list.Add(writer);
        }
        
        int j = 0; // index to move between files for writing

        while (!isMerged(readers_list))
        {
            int min_number = 150000;
            int index = -1;

            for (int i = 0; i < readers_list.Count; i++)
            {
                var bin_num = readers_list[i].GetCurrent();
                if (bin_num.Length > 0)
                {
                    var int_num = BitConverter.ToInt32(bin_num, 0);
                    if (sequence.Count == 0 || int_num >= sequence.Last())
                    {
                        if (int_num <= min_number)
                        {
                            min_number = int_num;
                            index = i;    
                        }
                    }
                }
            }
            if (index == -1)
            {
                foreach (var number in sequence)
                    writers_list[j].Write(BitConverter.GetBytes(number));
                sequence.Clear();
                j = (j + 1) % amount_of_files;
            }
            else
            {
                sequence.Add(min_number);
                if (sequence.Count > 100000)
                {
                    foreach (var number in sequence)
                        writers_list[j].Write(BitConverter.GetBytes(number));
                    sequence.Clear();
                }
                readers_list[index].GetNext();
            }
        }

        foreach (var number in sequence)
            writers_list[j].Write(BitConverter.GetBytes(number));

        foreach (var reader in readers_list)
            reader.Close();

        foreach (var writer in writers_list)
            writer.Close();
    }
    private static Boolean isMerged(List<Reader> readers_list)
    {
        foreach (var reader in readers_list)
        {
            if (reader.GetCurrent().Length != 0)
                return false;
        }
        return true;
    }

}