using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private AudioSource startAudio;
    private void loadMain(){
        // SceneManager.LoadScene("Main");//second���Ăяo���܂�
        SceneManager.LoadScene("TK_TEST");//second���Ăяo���܂�
    }
    public void change_button() //change_button�Ƃ������O�ɂ��܂�
    {
        startAudio.Play();
        Invoke(nameof(loadMain), 1.2f);
    }

    public void onGoal(){
        // string sceneName = "NextSceneHere";
        // SceneManager.LoadScene(sceneName)
        Debug.Log("onGoal");
        SceneManager.LoadScene("TK_TEST_GOAL");
    }

    private void OnTriggerEnter(Collider other){
        Debug.Log("OnTriggerEnter");
    }
}