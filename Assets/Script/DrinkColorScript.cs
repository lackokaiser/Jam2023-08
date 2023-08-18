using System;
using UnityEngine;

namespace Script
{
    public class DrinkColorScript : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private float fadeTime;
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

        public void InitFade(Color into)
        {
            changeTo = into;
            fadeTime += 10;
        }
    }
}