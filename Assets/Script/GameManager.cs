using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score;
    public LightManager lightManager;
    public UIManager uiManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //public void AddScore(int amount)
    //{
    //    score += amount;
    //    uiManager.UpdateScore(score);
    //}

    public void GameOver()
    {
        Debug.Log("Game Over");
        uiManager.ShowGameOver();
    }
}