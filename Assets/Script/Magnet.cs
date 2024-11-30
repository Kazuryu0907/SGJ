using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public bool isActive = false; // ���΂̃A�N�e�B�u���
    public bool isNorth = true;   // �k�ɂ���ɂ�

    void Start()
    {
        if (GetComponent<Collider>() == null)
        {
            gameObject.AddComponent<BoxCollider>(); // Collider��������Βǉ�
        }
    }

    // �N���b�N�����Ƃ��Ɏ��΂̏�Ԃ�؂�ւ���
    void OnMouseDown()
    {
        isActive = !isActive; // �N���b�N�Ŏ��΂̃A�N�e�B�u��Ԃ�؂�ւ���
        Debug.Log("Magnet Active: " + isActive);
    }
}
