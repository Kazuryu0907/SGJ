using UnityEngine;

public class Magnet : MonoBehaviour
{
    public bool isActive = false; // 磁石のアクティブ状態
    public GameManager.MagnetAttribute magnetAttribute; // 磁石の属性（正または負）

    void Start()
    {
        if (GetComponent<Collider>() == null)
        {
            gameObject.AddComponent<BoxCollider>(); // Colliderが無ければ追加
        }
    }

    // マウスを押したときにアクティブにする
    void OnMouseDown()
    {
        isActive = true; // アクティブ状態にする
        Debug.Log("Magnet Activated: " + isActive + ", Attribute: " + magnetAttribute);
    }

    // マウスを離したときに非アクティブにする
    void OnMouseUp()
    {
        isActive = false; // 非アクティブ状態にする
        Debug.Log("Magnet Deactivated: " + isActive + ", Attribute: " + magnetAttribute);
    }
}
