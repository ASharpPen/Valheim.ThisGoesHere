using System;
using System.IO;
using System.Linq;
using BepInEx;

namespace Valheim.ThisGoesHere;

internal static class PathHelper
{
    public static (string full, string partial) ExtractFilePath(string file)
    {
        var recombined = Path.Combine(file.SplitBySlash());
        return (Path.Combine(Paths.BepInExRootPath, recombined), recombined);
    }

    public static (string full, string relative) ExtractPath(string relativePath)
    {
        var relativeRecombined = Path.Combine(relativePath.SplitBySlash());
        return (Path.Combine(Paths.BepInExRootPath, relativeRecombined), relativeRecombined);
    }

    /// <summary>
    /// Gets the relative path from <paramref name="path"/> to <paramref name="relativeToPath"/>.
    /// </summary>
    /// <remarks>Assumes both paths are full paths, and that <paramref name="relativeToPath"/> is contained in <paramref name="path"/>.</remarks>
    public static string GetRelativePath(string path, string relativeToPath)
    {
        var pathParts = path.SplitBySlash();
        var relativeToParts = relativeToPath.SplitBySlash();

        for(int i = 0; i < relativeToParts.Length; ++i)
        {
            if (pathParts[i] != relativeToParts[i])
            {
                int relativePartsCount = pathParts.Length - i;
                var relativeParts = new string[relativePartsCount];
                Array.Copy(pathParts, i, relativeParts, 0, relativePartsCount);
                return Path.Combine(relativeParts);
            }
        }

        // path was fully matching relativeToPath.
        return path;
    }

    public static bool IsInsideBepInExFolder(string path)
    {
        return Path
            .GetFullPath(path)
            .StartsWith(Paths.BepInExRootPath);
    }

    public static void EnsureDirectoryExistsForFile(string filePath)
    {
        var dir = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(dir))
        {
            Log.LogTrace("Creating missing folders in path.");
            Directory.CreateDirectory(dir);
        }
    }
}
