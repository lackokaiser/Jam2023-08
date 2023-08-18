using Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorScript : MonoBehaviour
{
    public Color colorOfTheSprite;
    private DrinkScript finalDrink;
    private DrinkStorage Storage;
    int colorIndex = -1;
    // Start is called before the first frame update
    void Start()
    {
        colorOfTheSprite = GetComponent<SpriteRenderer>().color;

        
        if(colorOfTheSprite == Color.red)
        {
            colorIndex = 0;
        }else if(colorOfTheSprite == Color.green)
        {
            colorIndex = 1;
        }else if(colorOfTheSprite == Color.blue)
        {
            colorIndex = 2;
        }
        finalDrink = GameObject.FindWithTag("Drink").GetComponent<DrinkScript>();
        //Storage = GameObject.FindWithTag("storage").GetComponent<DrinkStorage>();
    }

    void OnMouseDown()
    {
        Debug.Log("Sikeres");
        finalDrink.AddColor(colorIndex);
    }

}
