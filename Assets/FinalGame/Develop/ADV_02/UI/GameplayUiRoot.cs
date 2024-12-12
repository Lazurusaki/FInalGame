using TMPro;
using UnityEngine;

namespace FinalGame.Develop.ADV_02.UI
{
    public class GameplayUiRoot : MonoBehaviour
    {
        //ADV_02
        [field: SerializeField] public InputFieldWithButton GuessValidateView { get; private set; }
        [field: SerializeField] public TMP_Text GeneratedSequence { get; private set; }
    }
}