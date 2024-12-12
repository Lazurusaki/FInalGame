using FinalGame.Develop.ADV_02.UI;
using FinalGame.Develop.CommonUI;
using UnityEngine;
using UnityEngine.UI;

namespace FinalGame.Develop.MainMenu.UI
{
    public class MainMenuUIRoot : MonoBehaviour
    {
        [field: SerializeField] public IconsWithTextList WalletView { get; private set; }
        
        [field: SerializeField] public Transform HUDLayer { get; private set; }
        [field: SerializeField] public Transform PopupsLayer { get; private set; }
        [field: SerializeField] public Transform VFXLayer { get; private set; }
        
        
        //ADV_02
        [field: SerializeField] public IconsWithTextList GameResultsStatsView { get; private set; }
        [field: SerializeField] public ToggleSwitchWithText GameModeToggle { get; private set; }
        [field: SerializeField] public Button StartGameButton { get; private set; }
        
        [field: SerializeField] public Button ResetStatsButton { get; private set; }
    }
}
