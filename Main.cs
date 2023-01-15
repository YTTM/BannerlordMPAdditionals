using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using HarmonyLib;

namespace BannerlordMPAdditionals
{
    public class BannerlordMPAdditionalsSubModule : MBSubModuleBase
    {
        public static BannerlordMPAdditionalsSubModule Instance { get; private set; }
        public Config configuration;

        private void setup()
        {
            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BannerlordMPAdditionals.json");
            if (!File.Exists(configPath))
            {
                configuration = new Config();
            }
            else
            {
                configuration = JsonConvert.DeserializeObject<Config>(File.ReadAllText(configPath));
            }
        }

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            this.setup();

            Debug.Print("[BMPA] BannerlordMPAdditionals by [14e] Yttrium Loaded", 0, Debug.DebugColor.Green);

            Harmony.DEBUG = true;
            var harmony = new Harmony("yttm.mpadditionals.bannerlord");

            if (configuration.SiegeNegativeMoraleDivider == 1)
            {
                Debug.Print("[BMPA] SiegeNegativeMoraleDivider : do nothing", 0, Debug.DebugColor.Green);
            }
            else
            {
                var original = typeof(MissionMultiplayerSiege).GetMethod("GetMoraleGain", BindingFlags.NonPublic | BindingFlags.Instance);
                // Debug.Print(original.ToString(), 0, Debug.DebugColor.Yellow);

                if (configuration.SiegeNegativeMoraleDivider == 0)
                {
                    var postfix = typeof(PatchMissionMultiplayerSiegeGetMoraleGain).GetMethod("GetMoraleGain0_Postfix");
                    // Debug.Print(postfix.ToString(), 0, Debug.DebugColor.Yellow);

                    harmony.Patch(original, postfix: new HarmonyMethod(postfix));
                    Debug.Print("[BMPA] SiegeNegativeMoraleDivider : fixed morale", 0, Debug.DebugColor.Green);
                }
                else
                {
                    if (configuration.SiegeNegativeMoraleDivider == 2)
                    {
                        var postfix = typeof(PatchMissionMultiplayerSiegeGetMoraleGain).GetMethod("GetMoraleGain2_Postfix");
                        // Debug.Print(postfix.ToString(), 0, Debug.DebugColor.Yellow);

                        harmony.Patch(original, postfix: new HarmonyMethod(postfix));
                        Debug.Print("[BMPA] SiegeNegativeMoraleDivider : 2", 0, Debug.DebugColor.Green);
                    }
                    else
                    {
                        Debug.Print("[BMPA] invalid SiegeNegativeMoraleDivider", 0, Debug.DebugColor.Red);
                    }
                }
            }
        }

        protected override void OnSubModuleUnloaded()
        {
            Debug.Print("[BMPA] Unloaded", 0, Debug.DebugColor.Green);
        }
    }
}
