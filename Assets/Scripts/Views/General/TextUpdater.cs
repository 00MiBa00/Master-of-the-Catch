using TMPro;
using UnityEngine;

namespace Views.General
{
    public class TextUpdater : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        private void OnDisable()
        {
            _text.text = string.Empty;
        }

        public void SetText(string value)
        {
            _text.text = string.Empty;
            _text.text = value;
        }
    }
}