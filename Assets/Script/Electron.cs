using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electron : MonoBehaviour
{
    public bool isNorth = false; // 電子が北極か南極か
    public float attractionForce = 5f; // 引き寄せる力
    public float repulsionForce = 5f; // 反発する力

    void Start()
    {
        // 初期設定として電子が南極の場合
        isNorth = false;
    }
}
