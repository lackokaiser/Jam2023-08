using Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorScript : MonoBehaviour
{
    Renderer FinalDrinkRenderer;
    Renderer MixDrinkRenderer;

    Vector4 colorOfFinalDrink;
    Vector4 colorOfMixDrink;
    // Start is called before the first frame update
    void Start()
    {
        MixDrinkRenderer = gameObject.GetComponent<Renderer>();
        
        MixDrinkRenderer.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        FinalDrinkRenderer = GameObject.FindWithTag("Drink").GetComponent<Renderer>();
        FinalDrinkRenderer.material.color = Color.white;

        Vector4 colorOfFinalDrink = FinalDrinkRenderer.material.color;
        Vector4 colorOfMixDrink = MixDrinkRenderer.material.color;
        
    }

    void OnMouseDown()
    {

        Debug.Log("Sikeres");

    }

}
