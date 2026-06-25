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

            // Harmony patches toepassen (nodig voor SuperPowerMod.cs om te werken!)
            new Harmony("stefan95228.cheats").PatchAll();

              // "Cheats" is de sectie-naam die je in de ConfigurationManager UI ziet.
            OneindigLevenEnabled = Config.Bind(
                "Cheats",
                "Oneindig Leven",
                true,
                "Schakel oneindig leven (health) in of uit."
            );
 
            OneindigLevenHotkey = Config.Bind(
                "Cheats",
                "Oneindig Leven Hotkey",
                new KeyboardShortcut(KeyCode.F1),
                "Toets om oneindig leven aan/uit te zetten tijdens het spelen."
            );
        }

        private void Update()
    {
        // Veiligheidscheck: doe niets als we nog in het hoofdmenu staan
        if (HeroController.instance == null) return;

        // Hotkey check: als de toets net is ingedrukt, wissel de instelling om.
        if (OneindigLevenHotkey.Value.IsDown())
        {
            OneindigLevenEnabled.Value = !OneindigLevenEnabled.Value;
            Logger.LogInfo($"Oneindig leven: {(OneindigLevenEnabled.Value ? "AAN" : "UIT")}");
        }

        // VOORBEELD 1: Oneindig leven
        if (OneindigLevenEnabled.Value)
        {
            int maxHealth = PlayerData.instance.GetInt("maxHealth");
            PlayerData.instance.SetInt("health", maxHealth);
        }
    }
    }
}
