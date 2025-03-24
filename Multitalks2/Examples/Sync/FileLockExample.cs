using System;
using System.IO;

class FileLockExample
{
    private static string LockFilePath = "/tmp/MyLockFile.lock";

    static void Main(string[] args)
    {
        using (FileStream fileStream = File.Open(LockFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
        {
            Console.WriteLine("Acquired file lock. Press Enter to release it.");
            Console.ReadLine();
        }

        Console.WriteLine("File lock released.");
    }
}