using System.Collections.Generic;
using System.Linq;
using Valheim.ThisGoesHere.Configs;
using Mono.Cecil;

namespace Valheim.ThisGoesHere;

internal static class Patcher
{
    public const string Name = "This Goes Here";
    public const string Version = "1.3.0";

    public static void Initialize()
    {
        var configs = ConfigLoader.Load();

        if (configs is null || configs.Count == 0)
        {
            Log.LogTrace($"Found no config files to run.");

#if DEBUG
            ExampleGenerator.PrintExample();
#endif
        }
        else
        {
            Log.LogTrace($"Found {configs.Count} config files.");

            foreach (var config in configs)
            {
                ConfigExecutor.Execute(config);
            }
        }
    }

    public static IEnumerable<string> TargetDLLs { get; } = Enumerable.Empty<string>();

    public static void Patch(AssemblyDefinition _) { }
}
