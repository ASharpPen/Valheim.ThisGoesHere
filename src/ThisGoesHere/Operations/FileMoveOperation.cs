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

        MoveRelative(config.From, config.To);
    }

    /// <param name="fromFile">Relative path to file to move.</param>
    /// <param name="toFile">Relative target to move file to.</param>
    public static void MoveRelative(string fromFile, string toFile)
    { 

        try
        {
            PathHelper.ExtractFilePath(fromFile, out string fromFull, out string fromPartial);

            if (!PathHelper.IsInsideBepInExFolder(fromFull))
            {
                Log.LogDebug($"Skipping move of file from '{fromPartial}'. File must be inside bepinex folder.");
                return;
            }

            if (!File.Exists(fromFull))
            {
                Log.LogDebug($"No file '{fromPartial}' found to move.");
                return;
            }

            PathHelper.ExtractFilePath(toFile, out string toFull, out string toPartial);

            if (toFull == fromFull)
            {
                return;
            }

            if (!PathHelper.IsInsideBepInExFolder(toFull))
            {
                Log.LogDebug($"Skipping move of file to '{toPartial}'. File must be inside bepinex folder.");
                return;
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
            Log.LogWarning($"Error while attempting to move file '{fromFile}' to '{toFile}'.", e);
        }
    }
}
