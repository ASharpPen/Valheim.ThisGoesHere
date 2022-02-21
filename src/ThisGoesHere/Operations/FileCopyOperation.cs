using System;
using System.IO;
using Valheim.ThisGoesHere.Configs;

namespace Valheim.ThisGoesHere.Operations;

internal static class FileCopyOperation
{
    public static void Execute(Config config)
    {
        config.CopyFile?.ForEach(x => Execute(x));
    }

    public static void Execute(FileCopyEntry config)
    {
        if (config is null)
        {
            return;
        }

        try
        {
            PathHelper.ExtractFilePath(config.From, out string fromFull, out string fromPartial);

            if (!PathHelper.IsInsideBepInExFolder(fromFull))
            {
                Log.LogDebug($"Skipping copy of file from '{fromPartial}'. File must be inside bepinex folder.");
                return;
            }

            if (!File.Exists(fromFull))
            {
                Log.LogDebug($"No file '{fromPartial}' found to copy.");
                return;
            }

            PathHelper.ExtractFilePath(config.To, out string toFull, out string toPartial);

            if (toFull == fromFull)
            {
                return;
            }

            if (!PathHelper.IsInsideBepInExFolder(toFull))
            {
                Log.LogDebug($"Skipping copy of file to '{toPartial}'. File must be inside bepinex folder.");
                return;
            }

            Log.LogInfo($"Copying '{fromPartial}' to '{toPartial}'");

            var dir = Path.GetDirectoryName(toFull);
            if (!Directory.Exists(dir))
            {
                Log.LogDebug("Creating missing folders in path.");
                Directory.CreateDirectory(dir);
            }

            File.Copy(fromFull, toFull, true);
        }
        catch (Exception e)
        {
            Log.LogWarning($"Error while attempting to copy file '{config.From}' to '{config.To}'.", e);
        }
    }
}
