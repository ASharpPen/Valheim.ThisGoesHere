using System.IO;

namespace Valheim.ThisGoesHere.Utilities;

internal static class DirectoryUtils
{
    public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
    {
        // Copy each file into it's new directory.
        foreach (FileInfo file in source.GetFiles())
        {
            var toFilePath = Path.Combine(target.FullName, file.Name);

            PathHelper.EnsureDirectoryExistsForFile(toFilePath);

            file.CopyTo(toFilePath, true);
        }

        // Copy each subdirectory using recursion.
        foreach (DirectoryInfo sourceSubDir in source.GetDirectories())
        {
            DirectoryInfo nextTargetSubDir =
                target.CreateSubdirectory(sourceSubDir.Name);
            CopyAll(sourceSubDir, nextTargetSubDir);
        }
    }
}
