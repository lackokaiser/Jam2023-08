
using Script.Extension;
using UnityEngine;

namespace Script
{
    public class GameManager : MonoBehaviour
    {
        [Min(0)]
        public int streak;
        public string playerTitle;

        public int pourAttempts;
        public Color colorToGo;
        public string potionName;
        public bool isDead;

        private DrinkStorage _storage;

        private DrinkScript mainDrink;

        public GameObject bottle;
        
        // Start is called before the first frame update
        void Start()
        {
            _storage = new DrinkStorage();
            mainDrink = GameObject.FindWithTag("Drink").GetComponent<DrinkScript>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public DrinkStorage GetDrinkStorage()
        {
            return _storage;
        }

        /**
         * Generates the next color goal and returns the starter color for the bottle
         */
        public void GenerateNextLevel()
        {
            System.Random r = new System.Random();

            Color starter = new Color((float)r.NextDouble(), (float)r.NextDouble(), (float)r.NextDouble());
            if (isDead)
                starter = starter.InvertColor();
            
            // add new potion if should
            if (streak % 5 == 0)
            {
                _storage.AddNewRandomDrink();
            }
            
            mainDrink.SetColorFade(starter);
            pourAttempts = r.Next(3, 8);
            for (int i = 0; i < pourAttempts; i++)
            {
                starter += _storage.GetDrinkColor(r.Next(_storage.GetDrinkAmount()), isDead);
            }

            colorToGo = starter;
            
            // TODO: potion name select
            
            
        }

        public bool IsColorMatch(Color other)
        {
            return colorToGo == other;
        }
    }
}
