using System;
using System.IO;
using BepInEx;

namespace Valheim.ThisGoesHere;

internal static class PathHelper
{
    public static (string full, string partial) ExtractFilePath(string file)
    {
        var recombined = Path.Combine(file.SplitBySlash());
        return (Path.Combine(Paths.BepInExRootPath, recombined), recombined);
    }

    public static bool IsInsideBepInExFolder(string path)
    {
        return Path
            .GetFullPath(path)
            .StartsWith(Paths.BepInExRootPath);
    }
}
