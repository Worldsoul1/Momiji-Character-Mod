using HarmonyLib;

namespace Momiji
{
    public static class PInfo
    {
        // each loaded plugin needs to have a unique GUID. usually author+generalCategory+Name is good enough
        public const string GUID = "worldsoul.test.momiji.mod";
        public const string Name = "Momiji Mod";
        public const string version = "0.0.1";
        public static readonly Harmony harmony = new Harmony(GUID);

    }
}
