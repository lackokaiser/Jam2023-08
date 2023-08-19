using Script.Extension;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class UiDrinkScript : MonoBehaviour
    {
        private Image _image;
        public float fadeTime = 10;
        private bool isChanging;
        private float fadeStart;
        private Color changeTo;

        private void Start()
        {
            _image = gameObject.GetComponent<Image>();
        }

        private void Update()
        {
            if (fadeStart < fadeTime)
            {
                fadeStart += Time.deltaTime * fadeTime;
 
                _image.color = Color.Lerp(_image.color, changeTo, fadeStart);
            }
        }

        public Color getCurrentColor()
        {
            return changeTo;
        }

        public void AddColor(Color c)
        {
            SetColor(_image.color.CombineColor(c));
        }

        public void AddColorFade(Color c)
        {
            InitFade(_image.color.CombineColor(c));
        }

        public void SetColor(Color c)
        {
            _image.color = c;
            changeTo = c;
        }

        public void InitFade(Color into)
        {
            changeTo = into;
            fadeStart = 0;
        }
    }
}