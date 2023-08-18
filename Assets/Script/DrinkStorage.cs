using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class DrinkStorage : MonoBehaviour
    {
        private List<Color> drinks = new();

        private void Start()
        {
            InitDrinks();
            
        }

        private void InitDrinks()
        {
            drinks.Add(Color.red);
            drinks.Add(Color.green);
            drinks.Add(Color.blue);
        }

        public Color GetDrinkColor(int index)
        {
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