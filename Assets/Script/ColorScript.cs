using Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorScript : MonoBehaviour
{
    public Color colorOfTheSprite;
    private GameObject FinalDrink;
    private DrinkScript finalDrink;
    private DrinkStorage Storage;
    Renderer FinalDrinkRenderer;
    Renderer MixDrinkRenderer;
    // Start is called before the first frame update
    void Start()
    {
        MixDrinkRenderer = gameObject.GetComponent<Renderer>();
        MixDrinkRenderer.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        FinalDrinkRenderer = GameObject.FindWithTag("Drink").GetComponent<Renderer>();
        FinalDrinkRenderer.material.color = Color.white;

        Vector4 colorOfTheFinalDrink = FinalDrinkRenderer.material.color;
        Vector4 colorOfTheMixDrink = MixDrinkRenderer.material.color;
        
    }

    void OnMouseDown()
    {
        Debug.Log("Sikeres");

    }

}
