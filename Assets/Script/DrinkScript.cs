using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class DrinkScript : MonoBehaviour
    {
        [SerializeField]
        private DrinkColorScript _colorScript;

        public Color getCurrentColor()
        {
            return _colorScript.getCurrentColor();
        }
        
        public void AddColor(Color c)
        {
            _colorScript.AddColorFade(c);
        }

        public void SetColorFade(Color c)
        {
            _colorScript.InitFade(c);
        }

        public void SetColor(Color c)
        {
            _colorScript.SetColor(c);
        }
    }
}