using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using HarmonyLib;

namespace BannerlordMPAdditionals
{
    class PatchMissionMultiplayerSiegeGetMoraleGain
    {
        public static int i = 0;
        public static void GetMoraleGain0_Postfix(ref int __result)
        {
            __result = 0;
        }
        public static void GetMoraleGain2_Postfix(ref int __result)
        {
            if (__result <= -2) // __ result ]-inf, -2]
            {
                __result /= 2;
            }
            else if(__result < 0) // __result = -1
            {
                i += __result;
                if(i <= -2)
                {
                    __result = i / 2;
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
