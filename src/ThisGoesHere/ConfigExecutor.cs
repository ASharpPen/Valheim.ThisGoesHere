using Valheim.ThisGoesHere.Configs;
using Valheim.ThisGoesHere.Operations;

namespace Valheim.ThisGoesHere;

internal static class ConfigExecutor
{
    public static void Execute(Config config)
    {
        if (!string.IsNullOrWhiteSpace(config.PrintComment))
        {
            Log.LogInfo(config.PrintComment);
        }

        FileCopyOperation.Execute(config);
        FileMoveOperation.Execute(config);
        FileDeleteOperation.Execute(config);

        DirectoryCopyOperation.Execute(config);
        DirectoryMoveOperation.Execute(config);
        DirectoryDeleteOperation.Execute(config);
    }
}
