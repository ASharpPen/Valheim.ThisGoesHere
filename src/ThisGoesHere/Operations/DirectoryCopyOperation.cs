using System;
using System.IO;
using BepInEx;
using Valheim.ThisGoesHere.Configs;
using Valheim.ThisGoesHere.Utilities;

namespace Valheim.ThisGoesHere.Operations;

internal static class DirectoryCopyOperation
{
    public static void Execute(Config config)
    {
        config.CopyFolder?.ForEach(x => Execute(x));
    }

    public static void Execute(FolderCopyEntry config)
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
                Log.LogDebug($"Skipping copying of folder from '{fromRelative}'. Folder must be inside bepinex folder.");
                return;
            }

            if (!Directory.Exists(fromFull))
            {
                Log.LogDebug($"No folder '{fromRelative}' found to copy.");
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
                Log.LogDebug($"Skipping copying of folder to '{toPartial}'. Folder must be inside bepinex folder.");
                return;
            }

            Log.LogInfo($"Copying '{fromRelative}' to '{toPartial}'");

            var files = Directory.GetFiles(fromFull, "*", SearchOption.AllDirectories);

            Log.LogTrace($"Found {files.Length} files to copy.");

            DirectoryUtils.CopyAll(new DirectoryInfo(fromFull), new DirectoryInfo(toFull));
        }
        catch (Exception e)
        {
            Log.LogWarning($"Error while attempting to copy folder '{config.From}' to '{config.To}'.", e);
        }
    }
}
