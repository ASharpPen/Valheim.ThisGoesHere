using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Valheim.ThisGoesHere.Extensions;

namespace Valheim.ThisGoesHere;

internal static class PathHelper
{
    internal static string RootPath { get; set; }

    public static void ExtractFilePath(string file, out string full, out string partial)
    {
        var recombined = Combine(file.SplitBySlash());

        full = Path.Combine(RootPath, recombined);
        partial = recombined;
    }

    public static void ExtractPath(string relativePath, out string full, out string relative)
    {
        var relativeRecombined = Combine(relativePath.SplitBySlash());

        full = Path.Combine(RootPath, relativeRecombined);
        relative = relativeRecombined;
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
                return Combine(relativeParts);
            }
        }

        // path was fully matching relativeToPath.
        return path;
    }

    public static bool IsInsideBepInExFolder(string path)
    {
        return Path
            .GetFullPath(path)
            .StartsWith(RootPath);
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

    public static string Combine(IEnumerable<string> paths)
    {
        string current = paths.FirstOrDefault();
        
        foreach (var path in paths.Skip(1))
        {
            current = Path.Combine(current, path);
        }

        return current;
    }
}
