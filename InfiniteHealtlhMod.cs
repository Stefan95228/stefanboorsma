using BepInEx;
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
    }
}

 


    

