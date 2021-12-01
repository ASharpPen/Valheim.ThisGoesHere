using System;
using System.IO;
using Valheim.ThisGoesHere.Configs;

namespace Valheim.ThisGoesHere.Operations;

internal static class FileMoveOperation
{
    public static void Execute(Config config)
    {
        config.MoveFile?.ForEach(x => Execute(x));
    }

    public static void Execute(FileMoveEntry config)
    {
        if (config is null)
        {
            return;
        }

        try
        {
            (string fromFull, string fromPartial) = PathHelper.ExtractFilePath(config.From);

            if (!PathHelper.IsInsideBepInExFolder(fromFull))
            {
                Log.LogDebug($"Skipping move of file from '{fromPartial}'. File must be inside bepinex folder.");
            }

            if (!File.Exists(fromFull))
            {
                Log.LogDebug($"No file '{fromPartial}' found to move.");
                return;
            }

            (string toFull, string toPartial) = PathHelper.ExtractFilePath(config.To);

            if (toFull == fromFull)
            {
                return;
            }

            if (!PathHelper.IsInsideBepInExFolder(toFull))
            {
                Log.LogDebug($"Skipping move of file to '{toPartial}'. File must be inside bepinex folder.");
            }

            Log.LogInfo($"Moving '{fromPartial}' to '{toPartial}'");

            var dir = Path.GetDirectoryName(toFull);
            if (!Directory.Exists(dir))
            {
                Log.LogDebug("Creating missing folders in path.");
                Directory.CreateDirectory(dir);
            }

            File.Copy(fromFull, toFull, true);
            File.Delete(fromFull);
        }
        catch (Exception e)
        {
            Log.LogWarning($"Error while attempting to move file '{config.From}' to '{config.To}'.", e);
        }
    }
}
