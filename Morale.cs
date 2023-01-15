using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using HarmonyLib;

namespace BannerlordMPAdditionals
{
    public class PatchMissionMultiplayerSiegeGetMoraleGain
    {
        private static int i = 0;
        public static int div = 0;
        public static void GetMoraleGain0_Postfix(ref int __result)
        {
            __result = 0;
        }
        public static void GetMoraleGainDiv_Postfix(ref int __result)
        {
            if(div <= 0)
                __result = 0;

            if (__result <= -div) // __ result ]-inf, -div]
            {
                __result /= div;
            }
            else if(__result < 0) // __result = -1
            {
                i += __result;
                if(i <= -div)
                {
                    __result = i / div;
                    i = 0;
                }
                else
                {
                    __result = 0;
                }
            }
        }
    }
}
