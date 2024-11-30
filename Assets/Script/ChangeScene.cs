using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void change_button() //change_button�Ƃ������O�ɂ��܂�
    {
        SceneManager.LoadScene("Main");//second���Ăяo���܂�
    }

    private void OnTriggerEnter(Collider other){
        Debug.Log("OnTriggerEnter");
    }
}