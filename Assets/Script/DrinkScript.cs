using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class DrinkScript : MonoBehaviour
    {
        List<Tuple<int, int>> listOfColors = new List<Tuple<int, int>>
        {
            Tuple.Create(1,0),
            Tuple.Create(2,0),
            Tuple.Create(3,0)
        };

        public void colorAdded()
        {
            GameObject drinkStorage = GameObject.FindWithTag("storage");
        }
    }
}