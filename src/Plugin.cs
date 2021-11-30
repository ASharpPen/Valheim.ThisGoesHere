using BepInEx;

namespace Valheim.ThisGoesHere
{
    [BepInPlugin(Guid, Name, Version)]
    internal class Plugin : BaseUnityPlugin
    {
        public const string Guid = "aaa.valheim.this_goes_here";
        public const string Name = "This Goes Here";
        public const string Version = "1.0.0";

        void Awake()
        {
            Log.Logger = Logger;

            FileMover.Run();
        }
    }
}
