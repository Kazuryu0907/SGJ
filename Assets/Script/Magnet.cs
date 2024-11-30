using UnityEngine;

public class Magnet : MonoBehaviour
{
    public bool isActive = false; // 磁石のアクティブ状態
    public GameManager.MagnetAttribute magnetAttribute; // 磁石の属性（正または負）
    private GameManager gameManager; // GameManagerの参照

    void Start()
    {
        // GameManagerの参照を取得
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManagerがシーンに見つかりません！");
        }

        if (GetComponent<Collider>() == null)
        {
            gameObject.AddComponent<BoxCollider>(); // Colliderが無ければ追加
        }
    }

    void Update()
    {
        // Qキーで磁石属性を正に設定
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (gameManager != null)
            {
                gameManager.magnetAttribute = GameManager.MagnetAttribute.Positive;
                Debug.Log("Magnet Attribute changed to Positive.");
            }
            else
            {
                Debug.LogWarning("gameManager is null");
            }
        }

        // Wキーで磁石属性を負に設定
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (gameManager != null)
            {
                gameManager.magnetAttribute = GameManager.MagnetAttribute.Negative;
                Debug.Log("Magnet Attribute changed to Negative.");
            }
            else
            {
                Debug.LogWarning("gameManager is null");
            }
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
