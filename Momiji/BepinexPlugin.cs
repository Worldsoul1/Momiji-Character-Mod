using BepInEx;
using HarmonyLib;
using LBoL.Base;
using LBoL.EntityLib.EnemyUnits.Character;
using LBoL.EntityLib.PlayerUnits;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Resource;
using Momiji.Source;
using Momiji;
using Momiji.Source.Localization;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


namespace Momiji
{
    [BepInPlugin(Momiji.PInfo.GUID, Momiji.PInfo.Name, Momiji.PInfo.version)]
    [BepInDependency(LBoLEntitySideloader.PluginInfo.GUID, BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency(AddWatermark.API.GUID, BepInDependency.DependencyFlags.SoftDependency)]
    [BepInProcess("LBoL.exe")]
    public class BepinexPlugin : BaseUnityPlugin
    {
        //The Unique mod ID of the mod.
        //WARNING: It is mandatory to rename it to avoid issues.
        public static string modUniqueID = "Momiji";
        //Name of the character.
        //This is also the prefix that is used before every .png file in DirResources. 
        public static string playerName = "Momiji";
        //Whether to us an ingame or custom model.
        //InGame: Will load the character model of the ingame character.
        //Custom: Will load DirResource/SampleCharacterModel.png 
        public static bool useInGameModel = false;
        //If InGame is selected, this is the model that will be loaded. 
        //Check LBoL.EntityLib.EnemyUnits.Character or using LBoL.EntityLib.PlayerUnits for a list of all the characters available. 
        public static string modelName = nameof(Youmu);
        //Some in-game model needs to be flipped (most notably elites).
        public static bool modelIsFlipped = true;
        //The character's off-color.
        //Used to separate cards in the card collection and put the off-color cards at the end.
        public static List<ManaColor> offColors = new List<ManaColor>() { ManaColor.White, ManaColor.Red, ManaColor.Black, ManaColor.Green, ManaColor.Blue, ManaColor.Colorless };

        private static readonly Harmony harmony = Momiji.PInfo.harmony;

        internal static BepInEx.Logging.ManualLogSource log;

        internal static TemplateSequenceTable sequenceTable = new TemplateSequenceTable();

        internal static IResourceSource embeddedSource = new EmbeddedSource(Assembly.GetExecutingAssembly());

        // add this for audio loading
        internal static DirectorySource directorySource = new DirectorySource(Momiji.PInfo.GUID, "");


        private void Awake()
        {
            log = Logger;

            // very important. Without this the entry point MonoBehaviour gets destroyed
            DontDestroyOnLoad(gameObject);
            gameObject.hideFlags = HideFlags.HideAndDontSave;

            CardIndexGenerator.PromiseClearIndexSet();
            EntityManager.RegisterSelf();

            harmony.PatchAll();

            if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey(AddWatermark.API.GUID))
                WatermarkWrapper.ActivateWatermark();
        }

        private void OnDestroy()
        {
            if (harmony != null)
                harmony.UnpatchSelf();
        }
    }
}