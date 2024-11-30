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
        SceneManager.LoadScene("TK_TEST_WALL");//second���Ăяo���܂�
        // gameOverScript = GameObject.Find("GameOver").GetComponent<GameOverScript>();
    }
    public void change_button() //change_button�Ƃ������O�ɂ��܂�
    {
        startAudio.Play();
        Invoke(nameof(loadMain), 1.2f);
    }

    public void resetScene(){
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void onGoal(){
        // string sceneName = "NextSceneHere";
        // SceneManager.LoadScene(sceneName)
        Debug.Log("onGoal");
        SceneManager.LoadScene("TK_TEST_GOAL");
    }

    public void OnGameOver(){
        Debug.Log("OnGameOver");
        gameOverScript.displayGameOver();
    }
    private void OnTriggerEnter(Collider other){
        Debug.Log("OnTriggerEnter");
    }

}