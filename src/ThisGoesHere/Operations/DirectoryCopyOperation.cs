using System;
using System.IO;
using BepInEx;
using Valheim.ThisGoesHere.Configs;

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
            (string fromFull, string fromRelative) = PathHelper.ExtractFilePath(config.From);

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

            (string toFull, string toPartial) = PathHelper.ExtractFilePath(config.To);

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

            foreach (var fromFullFilePath in files)
            {
                var toRelativeFilePath = PathHelper.GetRelativePath(fromFullFilePath, toFull);
                var toFullFilePath = Path.Combine(toFull, toRelativeFilePath);

                PathHelper.EnsureDirectoryExistsForFile(toFullFilePath);

                File.Copy(fromFullFilePath, toFullFilePath, true);
            }
        }
        catch (Exception e)
        {
            Log.LogWarning($"Error while attempting to copy folder '{config.From}' to '{config.To}'.", e);
        }
    }
}
