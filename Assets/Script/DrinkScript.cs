using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class DrinkScript : MonoBehaviour
    {
        private DrinkStorage Storage;
        [SerializeField]
        private DrinkColorScript _colorScript;

        
        private void Start()
        {
            Storage = GameObject.FindWithTag("storage").GetComponent<DrinkStorage>();

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