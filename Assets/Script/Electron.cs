using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electron : MonoBehaviour
{
    public bool isNorth = false; // �d�q���k�ɂ���ɂ�
    public float attractionForce = 5f; // �����񂹂��
    public float repulsionForce = 5f; // ���������

    void Start()
    {
        // �����ݒ�Ƃ��ēd�q����ɂ̏ꍇ
        isNorth = false;
    }
}
