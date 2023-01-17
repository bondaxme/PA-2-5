namespace lab1;

public class Reader
{
    private BinaryReader file;
    private byte[] current;
    private byte[] next;

    public Reader(string filename)
    {
        file = new BinaryReader(File.Open(filename, FileMode.Open));
        current = file.ReadBytes(4);
        next = file.ReadBytes(4);
    }

    public byte[] GetCurrent()
    {
        return current;
    }
    
    public byte[] GetNext()
    {
        byte[] temp = current;
        current = next;
        next = file.ReadBytes(4);
        return temp;
    }

    public void Close()
    {
        file.Close();
    }
}