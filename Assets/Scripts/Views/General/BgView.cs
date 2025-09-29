using UnityEngine;
using UnityEngine.UI;

namespace Views.General
{
    public class BgView : MonoBehaviour
    {
        private Image _bgImage;

        private void Awake()
        {
            SetBgImage();
        }

        public void SetSprite(Sprite sprite)
        {
            SetBgImage();
            
            _bgImage.sprite = sprite;
        }

        private void SetBgImage()
        {
            _bgImage ??= GetComponent<Image>();
        }
    }
}