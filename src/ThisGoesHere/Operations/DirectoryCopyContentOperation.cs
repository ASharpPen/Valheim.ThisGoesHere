using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Valheim.ThisGoesHere.Configs;
using Valheim.ThisGoesHere.Utilities;

namespace Valheim.ThisGoesHere.Operations;

internal static class DirectoryCopyContentOperation
{
    public static void Execute(Config config)
    {
        config.CopyFolderContent?.ForEach(x => Execute(x));
    }

    public static void Execute(FolderCopyContentEntry config)
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
                Log.LogDebug($"No folder '{fromRelative}' found to copy content from.");
                return;
            }

            PathHelper.ExtractFilePath(config.To, out string toFull, out string toPartial);

            var fromFolder = new DirectoryInfo(fromFull).Name;

            if (toFull == fromFull)
            {
                return;
            }

            if (!PathHelper.IsInsideBepInExFolder(toFull))
            {
                Log.LogDebug($"Skipping copying content of folder to '{toPartial}'. Folder must be inside bepinex folder.");
                return;
            }

            Log.LogInfo($"Copying content of '{fromRelative}' to '{toPartial}'");

            var files = Directory.GetFiles(fromFull, "*", SearchOption.AllDirectories);

            Log.LogTrace($"Found {files.Length} files to copy.");

            DirectoryUtils.CopyAll(new DirectoryInfo(fromFull), new DirectoryInfo(toFull));

        }
        catch (Exception e)
        {
            Log.LogWarning($"Error while attempting to copy folder content from '{config.From}' to '{config.To}'.", e);
        }
    }
}
