using System.Diagnostics;

namespace CSharpAdvanced.Tpl;

internal static class AppHelper
{
    public static Stopwatch StartStopwatch()
    {
        Stopwatch stopwatch = new();
        stopwatch.Start();
        return stopwatch;
    }

    public static void ShowFinalizedData(Stopwatch stopwatch)
    {
        stopwatch.Stop();
        var elapsedTime = stopwatch.Elapsed;
        Console.WriteLine($"Tempo decorrido: {elapsedTime.TotalSeconds} segundos");
    }
}