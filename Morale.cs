using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using HarmonyLib;

namespace BannerlordMPAdditionals
{
    class PatchMissionMultiplayerSiegeGetMoraleGain
    {
        public static void GetMoraleGain0_Postfix(ref int __result)
        {
            __result = 0;
        }
    }
}
