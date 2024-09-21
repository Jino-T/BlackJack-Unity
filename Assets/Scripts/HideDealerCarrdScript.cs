using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HideDealerCarrdScript : MonoBehaviour
{
    private static bool isRed = false;
    private static bool isBlue = false;
    private static bool isGreen = false;

    public Sprite redSprite;
    public Sprite blueSprite;
    public Sprite greenSprite;

    void Update()
    {
        //if(!isRed && !isGreen && !isBlue) {MakeBlue();}
        
        if (isRed) {gameObject.GetComponent<SpriteRenderer>().sprite = redSprite;}
        if (isGreen) {gameObject.GetComponent<SpriteRenderer>().sprite = greenSprite;}
        if (isBlue) {gameObject.GetComponent<SpriteRenderer>().sprite = blueSprite;}
        
    }

    public void MakeRed() {
        isRed = true;
        isGreen = false;
        isBlue = false;
    }
    public void MakeGreen() {
        isRed = false;
        isGreen = true;
        isBlue = false;
    }
    public void MakeBlue() {
        isRed = false;
        isGreen = false;
        isBlue = true;
    }
}