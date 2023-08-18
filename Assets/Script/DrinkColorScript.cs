using System;
using UnityEngine;

namespace Script
{
    public class DrinkColorScript : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        public float fadeTime = 10;
        private bool isChanging;
        private float fadeStart;
        private Color changeTo;

        private void Start()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (fadeStart < fadeTime)
            {
                fadeStart += Time.deltaTime * fadeTime;
 
                _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, changeTo, fadeStart);
            }
        }

        public void AddColor(Color c)
        {
            SetColor(_spriteRenderer.color + c);
        }

        public void AddColorFade(Color c)
        {
            InitFade(_spriteRenderer.color + c);
        }

        public void SetColor(Color c)
        {
            _spriteRenderer.color = c;
        }

        public void InitFade(Color into)
        {
            changeTo = into;
            fadeStart = 0;
        }
    }
}