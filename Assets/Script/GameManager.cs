
using Script.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

namespace Script
{
    public class GameManager : MonoBehaviour
    {
        [Min(0)]
        public int streak;
        private List<string> EnglishPotions = new List<string>();
        private List<string> GermanPotions = new List<string>();
        private string playerTitle;
        public int pourAttempts;
        public Color colorToGo;
        public string potionName;
        public int deadAmount;
        public bool isDead;

        private DrinkStorage _storage;

        private DrinkScript mainDrink;

        
        public GameObject bottle;

        public TextMeshProUGUI score;
        public TextMeshProUGUI title;
        
        // Start is called before the first frame update
        void Start()
        {
            _storage = new DrinkStorage();
            mainDrink = GameObject.FindWithTag("Drink").GetComponent<DrinkScript>();
            ReadPotionNames(EnglishPotions, @"Assets\Script\textfiles\potion_names.txt");
            ReadPotionNames(GermanPotions, @"Assets\Script\textfiles\potion_names_german.txt");
        }

        // Update is called once per frame
        void Update()
        {
            score.SetText(streak.ToString());
            title.SetText(playerTitle);
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
            string potionName;
            // TODO: potion name select
            int rndName = UnityEngine.Random.Range(1, 200);
            if (isDead)
            {
                potionName = GermanPotions[rndName];
            }
            else
            {
                potionName = EnglishPotions[rndName];
            }
            // TODO: Title giving
            
        }

        public bool IsColorMatch(Color other)
        {
            return colorToGo == other;
        }

        private void ReadPotionNames(List<string> list, string fileName)
        {
            StreamReader sr = new StreamReader(fileName);
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                list.Add(line);
            }
        }
    }
}
