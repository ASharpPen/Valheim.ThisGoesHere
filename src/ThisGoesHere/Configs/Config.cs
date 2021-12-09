using System.Collections.Generic;

namespace Valheim.ThisGoesHere.Configs;

public class Config
{
    /// <summary>
    /// Text to print before running operations.
    /// </summary>
    public string PrintComment { get; set; }

    public List<FileCopyEntry> CopyFile { get; set; }

    public List<FileMoveEntry> MoveFile { get; set; }

    public List<string> DeleteFile { get; set; }

    public List<FolderMoveEntry> MoveFolder { get; set; }

    public List<FolderCopyEntry> CopyFolder { get; set; }

    public List<string> DeleteFolder { get; set; }
}

public class FileCopyEntry
{
    public string From { get; set; }
    public string To { get; set; }
}

public class FileMoveEntry
{
    public string From { get; set; }
    public string To { get; set; }
}

public class FolderMoveEntry
{
    public string From { get; set; }
    public string To { get; set; }
}

public class FolderCopyEntry
{
    public string From { get; set; }
    public string To { get; set; }
}