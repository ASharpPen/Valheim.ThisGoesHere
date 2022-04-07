using System;

namespace Valheim.ThisGoesHere.Logging;

internal interface ILogger
{
    void LogTrace(string message);
    void LogDebug(string message);
    void LogInfo(string message);
    void LogWarning(string message);
    void LogWarning(string message, Exception e);
    void LogError(string message);
    void LogError(string message, Exception e);
}
