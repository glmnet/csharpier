using System.Diagnostics;

namespace CSharpier;

public class DebugLogger
{
    private static object lockObject = new();

    [Conditional("DEBUG")]
    public static void Log(string message)
    {
        lock (lockObject)
        {
            try
            {
                File.AppendAllText(@"C:\projects\csharpier\debug.txt", message + "\n");
            }
            catch (Exception)
            {
                // we don't care if this fails
            }
        }
    }
}
