using FinalGame.Develop.Gameplay;
using UnityEngine;

namespace FinalGame.Develop.ADV_02.Configs
{
    [CreateAssetMenu(menuName = "ADV_02/Configs/GameConfig", fileName = "GameConfig")]
    public class GameConfig : ScriptableObject
    {
        public int WinRewrd;
        public int LossPenalty;
    }
}