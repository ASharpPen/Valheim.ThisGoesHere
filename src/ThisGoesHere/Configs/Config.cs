using System.Collections.Generic;

namespace Valheim.ThisGoesHere.Configs;

public class Config
{
    public string PrintComment { get; set; }

    public List<FileCopyEntry> CopyFile { get; set; }

    public List<string> DeleteFile { get; set; }

    public List<FileMoveEntry> MoveFile { get; set; }
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