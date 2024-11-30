using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private AudioSource startAudio;
    [SerializeField] private GameOverScript gameOverScript;
    // private GameOverScript gameOverScript;
    private void loadMain(){
        // SceneManager.LoadScene("Main");//second���Ăяo���܂�
        SceneManager.LoadScene("1-1");//second���Ăяo���܂�
        // gameOverScript = GameObject.Find("GameOver").GetComponent<GameOverScript>();
    }
    public void change_button() //change_button�Ƃ������O�ɂ��܂�
    {
        startAudio.Play();
        Invoke(nameof(loadMain), 1.2f);
    }

    public void stage12()
    {
        SceneManager.LoadScene("1-2");
    }
    public void stage13()
    {
        SceneManager.LoadScene("1-3");
    }
    public void stage14()
    {
        SceneManager.LoadScene("1-4");
    }

    public void resetScene(){
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void onGoal()
    {
        Debug.Log("onGoal");
        SceneManager.LoadScene("GOAL");
    }

    public void Reset()
    {
        SceneManager.LoadScene("StageSelection");   
    }

    public void OnGameOver(){
        Debug.Log("OnGameOver");
        gameOverScript.displayGameOver();
    }
    private void OnTriggerEnter(Collider other){
        Debug.Log("OnTriggerEnter");
    }

}