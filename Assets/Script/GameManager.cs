
using JetBrains.Annotations;
using Script.Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Script
{
    public class GameManager : MonoBehaviour
    {
        [Range(0, 1)] public float ColorMatchThreshold = 0.3f;

        public TextAsset EffectNamesFile;
        public TextAsset EnglishTitlesFile;
        public TextAsset GermanTitlesFile;
        public TextAsset EnglishPotionsFile;
        public TextAsset GermanPotionsFile;
        [Min(0)]
        public int streak;
        private string playerTitle;

        private List<string> EnglishTitles = new List<string>();
        private List<string> GermanTitles = new List<string>();
        private List<string> EnglishPotions = new List<string>();
        private List<string> GermanPotions = new List<string>();
        private List<string> EffectNames = new();

        public int pourAttempts;
        public Color colorToGo;
        public string potionName;
        public int deadAmount;
        public bool isDead;

        private DrinkStorage _storage;
        private DrinkScript mainDrink;
        

        public GameObject bottle;

        public TextMeshProUGUI hitTheColor;
        public TextMeshProUGUI score;
        public TextMeshProUGUI title;
        public UiDrinkScript potion;
        public TextMeshProUGUI potionNm;
        public Animator playerAnimator;
        public Animator isHit;

        public Renderer ColorRenderer;
        public GameObject Potion;

        public Material GrayscaleMaterial;
        public Material InvertedMaterial;
        
        private static readonly int Dead1 = Animator.StringToHash("dead");
        private static readonly int Lived = Animator.StringToHash("lived");
        private static readonly int GrayscaleAmount = Shader.PropertyToID("_GrayscaleAmount");
        private static readonly int Threshold = Shader.PropertyToID("_Threshold");
        private static readonly int Hit = Animator.StringToHash("isHit");


        // Start is called before the first frame update
        void Start()
        {
            SetGrayscaleAmount(0);
            _storage = new DrinkStorage();
            mainDrink = GameObject.FindWithTag("Drink").GetComponent<DrinkScript>();
            ReadPotionsAndNames(EnglishPotions, EnglishPotionsFile);
            ReadPotionsAndNames(GermanPotions, GermanPotionsFile);
            ReadPotionsAndNames(EnglishTitles, EnglishTitlesFile);
            ReadPotionsAndNames(GermanTitles, GermanTitlesFile);
            ReadPotionsAndNames(EffectNames, EffectNamesFile);
            for (int i = 0; i < _storage.GetDrinkAmount(); i++)
            {
                GameObject tmp = Instantiate(Potion, new Vector3(i * 2.0f, -3.5f), Quaternion.identity);
                
                tmp.GetComponent<DrinkScript>().SetColorFade(_storage.GetDrinkColor(i));
            }

            GenerateNextLevel();
        }

        // Update is called once per frame
        void Update()
        {
            score.SetText(streak.ToString());
            title.SetText(playerTitle);
            potionNm.SetText(potionName);
        }

        public DrinkStorage GetDrinkStorage()
        {
            return _storage;
        }

        public void AddColorToDrink(Color c)
        {
            mainDrink.AddColor(c);
            pourAttempts--;
            if (pourAttempts == 0)
            {
                if (IsColorMatch(mainDrink.getCurrentColor()))
                {
                    mainDrink.PlayParticle();
                    if(!isDead)
                        streak++;
                    // TODO execute animation for scoring
                    hitTheColor.SetText(EffectNames[UnityEngine.Random.Range(0, EffectNames.Count-1)]);
                    hitTheColor.SetAllDirty();
                    isHit.SetTrigger(Hit);

                    if (isDead)
                        ToggleDead();
                }
                else if(!isDead)
                {
                    ToggleDead();
                }
                
                GenerateNextLevel();
            }
            else if (IsColorMatch(mainDrink.getCurrentColor()))
            {
                mainDrink.PlayParticle();
                if(!isDead)
                    streak++;
                // TODO execute animation for scoring
                hitTheColor.SetText(EffectNames[UnityEngine.Random.Range(0, EffectNames.Count-1)]);
                hitTheColor.SetAllDirty();
                isHit.SetTrigger(Hit);
                if (isDead)
                    ToggleDead();
                GenerateNextLevel();
            }
            

        }

        public void ToggleDead()
        {
            isDead = !isDead;
            playerAnimator.SetTrigger(isDead ? Dead1 : Lived);
            if (isDead)
                streak--;
            StartCoroutine(SetGrayscale(2f, isDead));
        }
        
        private IEnumerator SetInvertedRoutine(float duration, bool isGrayscale = true)
        {
            float time = 0;
            while (duration > time)
            {
                float durationTime = Time.deltaTime;
                float ratio = time / duration;
                
                SetInvertedAmount(isGrayscale ? ratio : 1 - ratio);
                time += durationTime;
                yield return null;
            }

            SetInvertedAmount(isGrayscale ? 1 : 0);
        }


        private void SetInvertedAmount(float amount)
        {
            InvertedMaterial.SetFloat(Threshold, amount);
        }

        private IEnumerator SetGrayscale(float duration, bool isGrayscale = true)
        {
            float time = 0;
            while (duration > time)
            {
                float durationTime = Time.deltaTime;
                float ratio = time / duration;
                
                SetGrayscaleAmount(isGrayscale ? ratio : 1 - ratio);
                time += durationTime;
                yield return null;
            }

            SetGrayscaleAmount(isGrayscale ? 1 : 0);
        }

        private void SetGrayscaleAmount(float amount)
        {
            GrayscaleMaterial.SetFloat(GrayscaleAmount, amount);
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

            if(streak % 2 == 0)
            {
                if (isDead)
                    playerTitle = GermanTitles[(streak % 5)];
                else
                    playerTitle = EnglishTitles[(streak % 5)];
            }

            
            mainDrink.SetColorFade(starter);
            pourAttempts = r.Next(3, 8);
            for (int i = 0; i < pourAttempts; i++)
            {
                starter = starter.CombineColor(_storage.GetDrinkColor(r.Next(_storage.GetDrinkAmount()), isDead));
            } 

            colorToGo = starter;
            potion.InitFade(colorToGo);
            
            int rndName = UnityEngine.Random.Range(1, 200);
            if (isDead)
               potionName = GermanPotions[rndName];
            else
               potionName = EnglishPotions[rndName];
            
        }

        public bool IsColorMatch(Color other)
        {
            return colorToGo.SimilarColor(other, ColorMatchThreshold);
        }

        private void ReadPotionsAndNames(List<string> list, TextAsset file)
        {
            list.AddRange(file.text.Split('\n').ToList());
        }
    }
}
