
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
        private string playerTitle;

        private List<string> EnglishTitles = new List<string>();
        private List<string> GermanTitles = new List<string>();
        private List<string> EnglishPotions = new List<string>();
        private List<string> GermanPotions = new List<string>();

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

        public Renderer ColorRenderer;
        public GameObject Potion;

        // Start is called before the first frame update
        void Start()
        {
            _storage = new DrinkStorage();
            mainDrink = GameObject.FindWithTag("Drink").GetComponent<DrinkScript>();
            Potion = GameObject.FindWithTag("Drink");
            ReadPotionsAndNames(EnglishPotions, @"Assets\Script\textfiles\potion_names.txt");
            ReadPotionsAndNames(GermanPotions, @"Assets\Script\textfiles\potion_names_german.txt");
            ReadPotionsAndNames(EnglishTitles, @"Assets\Script\textfiles\player_levels.txt");
            ReadPotionsAndNames(GermanTitles, @"Assets\Script\textfiles\player_levels_german.txt");

            for(int i = 0; i < _storage.GetDrinkAmount(); i++)
            {
                GameObject tmp = Instantiate(Potion, new Vector3(i * 2.0f, 1.0f), Quaternion.identity);
                tmp.GetComponent<SpriteRenderer>().color = _storage.GetDrinkColor(i);
            }

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
                if (isDead)
                   playerTitle = GermanTitles[(streak % 5)];
                else
                   playerTitle = EnglishTitles[(streak % 5)];
            }
            
            mainDrink.SetColorFade(starter);
            pourAttempts = r.Next(3, 8);
            for (int i = 0; i < pourAttempts; i++)
            {
                starter += _storage.GetDrinkColor(r.Next(_storage.GetDrinkAmount()), isDead);
            }

            colorToGo = starter;
            
            int rndName = UnityEngine.Random.Range(1, 200);
            if (isDead)
               potionName = GermanPotions[rndName];
            else
               potionName = EnglishPotions[rndName];
            
        }

        public bool IsColorMatch(Color other)
        {
            return colorToGo == other;
        }

        private void ReadPotionsAndNames(List<string> list, string fileName)
        {
            StreamReader sr = new StreamReader(fileName);
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                list.Add(line);
            }
            Debug.Log("Fasz");
        }
    }
}
