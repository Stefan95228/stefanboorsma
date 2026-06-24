<<<<<<< HEAD
using BepInEx;
using UnityEngine;
using HarmonyLib;

namespace Stefan95228
{
    [HarmonyPatch(typeof(HealthManager), "TakeDamage")]
    public static class HealthManager_TakeDamage_Patch
    {
        // Prefix: loopt VOOR de originele functie. Door hp hier al naar 0 te zetten,
        // laten we de game zelf de "dood" animatie/logica afhandelen zoals normaal.
        public static void Prefix(HealthManager __instance)
        {
            // Niet de speler zelf one-shotten (extra veiligheid).
            if (__instance.gameObject.GetComponent<HeroController>() != null) return;

            __instance.hp = 0;
        }
    }
}
=======
using BepInEx;
using UnityEngine;
using HarmonyLib;

namespace Stefan95228
{
    [HarmonyPatch(typeof(HealthManager), "TakeDamage")]
    public static class HealthManager_TakeDamage_Patch
    {
        // Prefix: loopt VOOR de originele functie. Door hp hier al naar 0 te zetten,
        // laten we de game zelf de "dood" animatie/logica afhandelen zoals normaal.
        public static void Prefix(HealthManager __instance)
        {
            // Niet de speler zelf one-shotten (extra veiligheid).
            if (__instance.gameObject.GetComponent<HeroController>() != null) return;

            __instance.hp = 0;
        }
    }
}
>>>>>>> 82c650420fc378a49f3bfa4af6248827eb17281a
