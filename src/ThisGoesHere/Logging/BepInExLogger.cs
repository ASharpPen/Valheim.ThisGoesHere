using System;
using BepInEx.Logging;

namespace Valheim.ThisGoesHere.Logging;

internal class BepInExLogger : ILogger
{
    private ManualLogSource Logger { get; set; }

    public BepInExLogger()
    {
        Logger = BepInEx.Logging.Logger.CreateLogSource(Patcher.Name);
    }

    public void LogTrace(string message)
    {
        Logger?.LogDebug($"{message}");
    }

    public void LogDebug(string message)
    {
        Logger?.LogInfo($"{message}");
    }

    public void LogInfo(string message) => Logger?.LogMessage($"{message}");

    public void LogWarning(string message) => Logger?.LogWarning($"{message}");

    public void LogWarning(string message, Exception e) => Logger?.LogWarning($"{message}\n{e}");

    public void LogError(string message) => Logger?.LogError($"{message}");

    public void LogError(string message, Exception e) => Logger?.LogError($"{message}\n{e}");
}
