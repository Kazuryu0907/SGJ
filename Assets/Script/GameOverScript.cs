using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    // Start is called before the first frame update
    CanvasGroup canvasGroup;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        hideGameOver();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void displayGameOver(){
        // Debug.Log("GameOverScript.displayGameOver");
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        // gameObject.SetActive(true);
    }
    public void hideGameOver(){
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        // gameObject.SetActive(false);
    }
}
