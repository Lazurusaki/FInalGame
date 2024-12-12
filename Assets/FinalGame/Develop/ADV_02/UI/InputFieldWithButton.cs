using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FinalGame.Develop.ADV_02.UI
{
    public class InputFieldWithButton : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _field;
        [SerializeField] private Button _button;

        public string GetText => _field.text;

        public Button GetButton =>_button;
    }
}