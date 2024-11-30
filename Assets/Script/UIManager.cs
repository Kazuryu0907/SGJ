using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    public GameObject gameOverPanel;

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }
}