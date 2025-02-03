using FinalGame.Develop.CommonUI;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.UI
{
    public class GameplayUIRoot : MonoBehaviour
    {
        [field: SerializeField] public Transform HUDLayer { get; private set; }
        [field: SerializeField] public Transform HUDWorldLayer { get; private set; }
        
        [field: SerializeField] public Transform PopupsLayer { get; private set; }
        [field: SerializeField] public Transform VFXLayer { get; private set; }
    }
}