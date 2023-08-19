using System;
using UnityEngine;

namespace Script
{
    public class DrinkMixtureScript : MonoBehaviour
    {
        private GameManager gameManager;
        private DrinkScript drink;

        private void Start()
        {
            gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
            drink = gameObject.GetComponent<DrinkScript>();
        }

        private void OnMouseUpAsButton()
        {
            gameManager.AddColorToDrink(drink.getCurrentColor());
        }
    }
}