using System;
using System.Collections.Generic;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using BepInEx;
using System.IO;

namespace Valheim.ThisGoesHere.Configs;

internal static class ConfigLoader
{
    public static List<Config> Load()
    {
        var configPaths = Directory.GetFiles(Paths.ConfigPath, Constants.ConfigName, SearchOption.AllDirectories);

        if (configPaths is null || configPaths.Length == 0)
        {
            return new(0);
        }

        var deserializer = new DeserializerBuilder()
           .WithNamingConvention(PascalCaseNamingConvention.Instance)
           .IgnoreUnmatchedProperties()
           .Build();

        List<Config> configs = new(configPaths.Length);

        foreach (var file in configPaths)
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

            if (config != null)
            {
                configs.Add(config);

#if TRUE && DEBUG
                var serializer = new SerializerBuilder()
                    .WithNamingConvention(PascalCaseNamingConvention.Instance)
                    .Build();

                Log.LogTrace("\n" + serializer.Serialize(config));
#endif
            }
        }

        return configs;
    }
}
