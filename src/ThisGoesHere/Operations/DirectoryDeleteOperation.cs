using System;
using System.IO;
using Valheim.ThisGoesHere.Configs;
using Valheim.ThisGoesHere.Extensions;

namespace Valheim.ThisGoesHere.Operations;

internal static class DirectoryDeleteOperation
{
    public static void Execute(Config config)
    {
        config.DeleteFolder?.ForEach(x => Execute(x));
    }

    public static void Execute(string path)
    {
        if (path.IsEmpty())
        {
            return;
        }

        try
        {
            PathHelper.ExtractFilePath(path, out string pathFull, out string pathRelative);

            if (!PathHelper.IsInsideBepInExFolder(pathFull))
            {
                Log.LogDebug($"Skipping deletion of folder '{pathRelative}'. Folder must be inside bepinex folder.");
                return;
            }

            if (!Directory.Exists(pathFull))
            {
                return;
            }

            Log.LogInfo($"Deleting folder '{pathRelative}'");

            Directory.Delete(pathFull, true);
        }
        catch (Exception e)
        {
            Log.LogWarning($"Error while attempting to delete folder '{path}'.", e);
        }
    }
}
