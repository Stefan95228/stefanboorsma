using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace Stefan95228
{
    // De generator (AutoPlugin) maakt op de achtergrond zelf het [BepInPlugin] label aan.
    // Door 'partial' te gebruiken, plakken we onze cheat-code aan die gegenereerde code vast.
    [BepInAutoPlugin]
    public partial class Stefan95228Plugin : BaseUnityPlugin
    {
        private bool _oneindigLeven = true;

        private void Awake()
        {
            Logger.LogInfo("Stefan's Cheats Mod is succesvol opgestart via AutoPlugin!");

            // Harmony patches toepassen (voor de one-hit-kill, zie class onderaan dit bestand)
            new Harmony("stefan95228.cheats").PatchAll();
        }

        private void Update()
        {
            // Veiligheidscheck: doe niets als we nog in het hoofdmenu staan
            if (HeroController.instance == null) return;

            // VOORBEELD 1: Oneindig leven
            if (_oneindigLeven)
            {
                int maxHealth = PlayerData.instance.GetInt("maxHealth");
                PlayerData.instance.SetInt("health", maxHealth);
            }
        }
    }

    // Patcht HealthManager.TakeDamage zodat vijanden in 1 hit doodgaan.
    // (De oude aanpak met PlayerData "nailDamage" werkte niet, omdat Silksong
    // schade niet op die manier berekent.)
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