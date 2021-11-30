using System;
using System.Collections.Generic;
using System.IO;
using BepInEx;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;

namespace Valheim.ThisGoesHere;

internal static class FileMover
{
    private const string ConfigName = "Valheim.ThisGoesHere.yaml";

    internal static void Run()
    {
        var configs = Directory.GetFiles(Paths.ConfigPath, ConfigName, SearchOption.AllDirectories);

        if (configs.Length == 0)
        {
            PrintExample();
        }
        else
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(PascalCaseNamingConvention.Instance)
                .IgnoreUnmatchedProperties()
                .Build();

            Dictionary<string, HashSet<string>> moves = new();

            foreach (var file in configs)
            {
                Config config = null;
                try
                {
                    config = deserializer.Deserialize<Config>(File.ReadAllText(file));
                }
                catch (Exception e)
                {
                    Log.LogWarning($"Unable to parse config file '{file}'.", e);
                    continue;
                }

                foreach (var entry in config.Entries ?? new(0))
                {
                    if (moves.ContainsKey(entry.FromFile))
                    {
                        moves[entry.FromFile].Add(entry.ToFile);
                    }
                    else
                    {
                        moves[entry.FromFile] = new() { entry.ToFile };
                    }
                }
            }

            foreach (var fromFilePairs in moves)
            {
                MoveFile(fromFilePairs.Key, fromFilePairs.Value);
            }
        }
    }

    private static void MoveFile(string fromFile, IEnumerable<string> toFiles)
    {
        (string fromFull, string fromPartial) = ExtractFilePath(fromFile);

        if (!File.Exists(fromFull))
        {
            Log.LogDebug($"No file '{fromPartial}' found to move.");
            return;
        }

        foreach (var toFile in toFiles)
        {
            (string toFull, string toPartial) = ExtractFilePath(toFile);

            Log.LogInfo($"Copying '{fromPartial}' to '{toPartial}'");

            var dir = Path.GetDirectoryName(toFull);
            if (!Directory.Exists(dir))
            {
                Log.LogDebug("Creating missing folders in path.");
                Directory.CreateDirectory(dir);
            }

            File.Copy(fromFull, toFull, true);
        }
    }

    private static (string full, string partial) ExtractFilePath(string file)
    {
        var recombined = Path.Combine(file.Split('/', '\\'));
        return (Path.Combine(Paths.BepInExRootPath, recombined), recombined);
    }

    private static void PrintExample()
    {
        var serializer = new SerializerBuilder()
               .WithNamingConvention(PascalCaseNamingConvention.Instance)
               .Build();

        var exampleFile = serializer.Serialize(new Config
        {
            Entries = new List<ConfigEntry>
            {
                new ConfigEntry
                {
                    FromFile = "config/" + ConfigName,
                    ToFile = "config/" + ConfigName
                }
            }
        });

        var filePath = Path.Combine(Paths.ConfigPath, ConfigName);

        File.WriteAllText(filePath, exampleFile);
    }
}
