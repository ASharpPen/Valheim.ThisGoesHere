using System;

namespace Valheim.ThisGoesHere.Logging;

internal class ConsoleLogger : ILogger
{
    public void LogDebug(string message) => Console.WriteLine($"[Debug] {message}");
    public void LogError(string message) => Console.WriteLine($"[Error] {message}");
    public void LogError(string message, Exception e) => Console.WriteLine($"[Error] {message}\n{e}");
    public void LogInfo(string message) => Console.WriteLine($"[Info] {message}");
    public void LogTrace(string message) => Console.WriteLine($"[Trace] {message}");
    public void LogWarning(string message) => Console.WriteLine($"[Warning] {message}");
    public void LogWarning(string message, Exception e) => Console.WriteLine($"[Warning] {message}\n{e}");
}
