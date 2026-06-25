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
      
      
       // Eigen ConfigEntry voor deze cheat, los van InfiniteHealthMod.
        // Verschijnt ook automatisch in de ConfigurationManager UI, in dezelfde "Cheats" sectie.
        public static ConfigEntry<bool> OneHitKillEnabled;
        public static ConfigEntry<KeyboardShortcut> OneHitKillHotkey;
 
        // Wordt aangeroepen vanuit InfiniteHealthMod.cs's Awake(), zie de regel daar.
        public static void Initialize(BepInEx.Configuration.ConfigFile config)
        {
            OneHitKillEnabled = config.Bind(
                "Cheats",
                "One Hit Kill",
                true,
                "Schakel one-hit-kill (super schade) in of uit."
            );
 
            OneHitKillHotkey = config.Bind(
                "Cheats",
                "One Hit Kill Hotkey",
                new KeyboardShortcut(KeyCode.F2),
                "Toets om one-hit-kill aan/uit te zetten tijdens het spelen."
            );
        }
 
        // Wordt elke frame gecheckt vanuit InfiniteHealthMod.cs's Update(), zie de regel daar.
        public static void CheckHotkey()
        {
            if (OneHitKillHotkey.Value.IsDown())
            {
                OneHitKillEnabled.Value = !OneHitKillEnabled.Value;
            }
        }
      
       public static void Prefix(HealthManager __instance)
        {
            // Niet de speler zelf one-shotten (extra veiligheid).
            if (__instance.gameObject.GetComponent<HeroController>() != null) return;

            __instance.hp = 0;
        }
   }
}

