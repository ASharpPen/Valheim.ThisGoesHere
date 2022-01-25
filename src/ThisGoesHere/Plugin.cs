using BepInEx;
using Valheim.ThisGoesHere.Configs;

namespace Valheim.ThisGoesHere;

[BepInPlugin(Guid, Name, Version)]
internal class Plugin : BaseUnityPlugin
{
    public const string Guid = ".valheim.this_goes_here";
    public const string Name = "This Goes Here";
    public const string Version = "1.2.1";

    void Awake()
    {
        Log.Logger = Logger;

        Run();
    }

    private void Run()
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
}
