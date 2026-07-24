using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

[BepInPlugin("HazardModSilk", "HazardModSilk", "1.0.0")]
public class HazardModSilk : BaseUnityPlugin
{

    private void Awake()
    {
        logger.LogInfo("Plugin loadeed and initialized.");

        Harmony.CreateAndPatchAll(typeof(HazardModSilk), null);
    }


    [HarmonyPostfix]
    [HarmonyPatch(typeof(PlayerData), "AddGeo")]

    private static void AddGeoPostfix(PlayerData __instance, int amount)
    {
        __instance.geo += amount; // Add geo one more time
    }
}    
