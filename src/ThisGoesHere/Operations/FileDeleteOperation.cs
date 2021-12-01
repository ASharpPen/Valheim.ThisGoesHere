using System;
using System.IO;
using Valheim.ThisGoesHere.Configs;

namespace Valheim.ThisGoesHere.Operations;

internal static class FileDeleteOperation
{
    public static void Execute(Config config)
    {
        config.DeleteFile?.ForEach(x => Execute(x));
    }

    public static void Execute(string file)
    {
        if (string.IsNullOrWhiteSpace(file))
        {
            return;
        }

        try
        {
            (string pathFull, string pathPartial) = PathHelper.ExtractFilePath(file);

            if (!PathHelper.IsInsideBepInExFolder(pathFull))
            {
                Log.LogDebug($"Skipping deletion of file from '{pathPartial}'. File must be inside bepinex folder.");
                return;
            }

            if (!File.Exists(pathFull))
            {
                return;
            }

            Log.LogInfo($"Deleting file '{pathPartial}'");

            File.Delete(pathFull);
        }
        catch (Exception e)
        {
            Log.LogWarning($"Error while attempting to delete file '{file}'.", e);
        }
    }
}
