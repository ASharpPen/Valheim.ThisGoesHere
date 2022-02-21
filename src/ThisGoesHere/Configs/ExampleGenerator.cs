using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using BepInEx;

namespace Valheim.ThisGoesHere.Configs;

internal static class ExampleGenerator
{
    public static void PrintExample()
    {
        var serializer = new SerializerBuilder()
               .WithNamingConvention(PascalCaseNamingConvention.Instance)
               .Build();

        var exampleFile = serializer.Serialize(new Config
        {
            CopyFile = new List<FileCopyEntry>
                {
                    new FileCopyEntry
                    {
                        From = "config/" + Constants.ConfigName,
                        To = "config/" + Constants.ConfigName + ".test"
                    },
                },
            MoveFile = new List<FileMoveEntry>
                {
                    new FileMoveEntry
                    {
                        From = "config/" + Constants.ConfigName,
                        To = "config/" + Constants.ConfigName
                    }
                },
            DeleteFile = new()
            {
                "config/" + Constants.ConfigName + ".test"
            }
        });

        var filePath = Path.Combine(Paths.ConfigPath, Constants.ConfigName);

        File.WriteAllText(filePath, exampleFile);
    }
}
