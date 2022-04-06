using Valheim.ThisGoesHere.Configs;
using Valheim.ThisGoesHere.Extensions;
using Valheim.ThisGoesHere.Operations;

namespace Valheim.ThisGoesHere;

internal static class ConfigExecutor
{
    public static void Execute(Config config)
    {
        if (!config.PrintComment.IsEmpty())
        {
            Log.LogInfo(config.PrintComment);
        }

        FileCopyOperation.Execute(config);
        FileMoveOperation.Execute(config);
        FileDeleteOperation.Execute(config);

        DirectoryCopyOperation.Execute(config);
        DirectoryCopyContentOperation.Execute(config);
        DirectoryMoveOperation.Execute(config);
        DirectoryDeleteOperation.Execute(config);
    }
}
