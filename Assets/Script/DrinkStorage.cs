using System.Collections.Generic;
using Script.Extension;
using UnityEngine;

namespace Script
{
    public class DrinkStorage
    {
        private List<Color> drinks = new();

        public DrinkStorage()
        {
            InitDrinks();
            
        }

        private void InitDrinks()
        {
            drinks.Add(Color.red);
            drinks.Add(Color.green);
            drinks.Add(Color.blue);
        }

        public Color GetDrinkColor(int index, bool inverted = false)
        {
            if (inverted)
                return drinks[index].InvertColor();
            return drinks[index];
        }

        public int GetDrinkAmount()
        {
            return drinks.Count;
        }

        public void AddNewRandomDrink()
        {
            System.Random r = new System.Random();
            drinks.Add(new Color((float)r.NextDouble(), (float)r.NextDouble(), (float)r.NextDouble()));
        }
    }
}