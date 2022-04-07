using System;
using Valheim.ThisGoesHere.Logging;

namespace Valheim.ThisGoesHere;

internal static class Log
{
    internal static ILogger Logger;

    public static void LogTrace(string message) => Logger.LogTrace(message);
    public static void LogDebug(string message) => Logger?.LogDebug(message);
    public static void LogInfo(string message) => Logger?.LogInfo(message);
    public static void LogWarning(string message) => Logger.LogWarning(message);
    public static void LogWarning(string message, Exception e) => Logger.LogWarning(message, e);
    public static void LogError(string message) => Logger?.LogError(message);
    public static void LogError(string message, Exception e) => Logger?.LogError(message, e);
}
