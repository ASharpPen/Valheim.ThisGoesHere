using System;
using System.IO;
using Valheim.ThisGoesHere.Configs;
using Valheim.ThisGoesHere.Utilities;

namespace Valheim.ThisGoesHere.Operations;

internal static class DirectoryMoveOperation
{
    public static void Execute(Config config)
    {
        config.MoveFolder?.ForEach(x => Execute(x));
    }

    public static void Execute(FolderMoveEntry config)
    {
        if (config is null)
        {
            return;
        }

        try
        {
            PathHelper.ExtractFilePath(config.From, out string fromFull, out string fromRelative);

            if (!PathHelper.IsInsideBepInExFolder(fromFull))
            {
                Log.LogDebug($"Skipping move of folder from '{fromRelative}'. Folder must be inside bepinex folder.");
                return;
            }

            if (!Directory.Exists(fromFull))
            {
                Log.LogDebug($"No folder '{fromRelative}' found to move.");
                return;
            }

            PathHelper.ExtractFilePath(config.To, out string toFull, out string toPartial);

            var fromFolder = new DirectoryInfo(fromFull).Name;
            toFull = Path.Combine(toFull, fromFolder);
            toPartial = Path.Combine(toPartial, fromFolder);

            if (toFull == fromFull)
            {
                return;
            }

            if (!PathHelper.IsInsideBepInExFolder(toFull))
            {
                Log.LogDebug($"Skipping move of folder to '{toPartial}'. Folder must be inside bepinex folder.");
                return;
            }

            Log.LogInfo($"Moving '{fromRelative}' to '{toPartial}'");

            var files = Directory.GetFiles(fromFull, "*", SearchOption.AllDirectories);

            Log.LogTrace($"Found {files.Length} files to move.");

            DirectoryUtils.CopyAll(new DirectoryInfo(fromFull), new DirectoryInfo(toFull));

            Directory.Delete(fromFull, true);
        }
        catch (Exception e)
        {
            Log.LogWarning($"Error while attempting to move folder '{config.From}' to '{config.To}'.", e);
        }
    }
}
