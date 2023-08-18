using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class DrinkScript : MonoBehaviour
    {
        private List<(int, int)> listOfColors = new();

        private DrinkStorage Storage;
        [SerializeField]
        private DrinkColorScript _colorScript;

        
        private void Start()
        {
            Storage = GameObject.FindWithTag("storage").GetComponent<DrinkStorage>();

        }

        public void AddColor(int addedIndex)
        {
            
        }
    }
}