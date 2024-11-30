using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public bool isActive = false; // 磁石のアクティブ状態
    public bool isNorth = true;   // 北極か南極か

    void Start()
    {
        if (GetComponent<Collider>() == null)
        {
            gameObject.AddComponent<BoxCollider>(); // Colliderが無ければ追加
        }
    }

    // クリックしたときに磁石の状態を切り替える
    void OnMouseDown()
    {
        isActive = !isActive; // クリックで磁石のアクティブ状態を切り替える
        Debug.Log("Magnet Active: " + isActive);
    }
}
