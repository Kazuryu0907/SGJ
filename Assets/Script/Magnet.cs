using UnityEngine;

public class Magnet : MonoBehaviour
{
    public bool isActive = false; // 現在のアクティブ状態
    public GameManager.MagnetAttribute magnetAttribute; // 磁石の属性（正または負）
    private GameManager gameManager; // GameManagerへの参照

    private Animator animator; // アニメーションを制御するためのAnimator
    private PlayerController playerController; // プレイヤーの状態を確認するための参照

    void Start()
    {
        // GameManagerの参照を取得
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManagerがシーンに存在しません！");
        }

        // プレイヤーの状態を確認
        playerController = FindObjectOfType<PlayerController>();

        if (playerController == null)
        {
            Debug.LogError("PlayerControllerがシーンに存在しません！");
        }

        // Animatorコンポーネントを取得
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animatorがアタッチされていません！");
        }

        // 必要に応じてBoxCollider2Dを追加
        if (GetComponent<Collider2D>() == null)
        {
            gameObject.AddComponent<BoxCollider2D>();
        }
    }

    void Update()
    {
        // Qキーで磁石の属性をポジティブに設定
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (gameManager != null)
            {
                gameManager.magnetAttribute = GameManager.MagnetAttribute.Positive;
                magnetAttribute = GameManager.MagnetAttribute.Positive;
                Debug.Log("Magnet Attribute changed to Positive.");
            }
            else
            {
                Debug.LogWarning("gameManager is null");
            }
        }

        // Wキーで磁石の属性をネガティブに設定
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (gameManager != null)
            {
                gameManager.magnetAttribute = GameManager.MagnetAttribute.Negative;
                magnetAttribute = GameManager.MagnetAttribute.Negative;
                Debug.Log("Magnet Attribute changed to Negative.");
            }
            else
            {
                Debug.LogWarning("gameManager is null");
            }
        }
    }

    // マウスが押されたときの処理
    void OnMouseDown()
    {
        // プレイヤーの色を確認
        if (playerController != null)
        {
            bool isRed = playerController.IsRed();
            bool isBlue = playerController.IsBlue();

            if (isRed)
            {
                isActive = true; // アクティブ状態に設定
                animator.SetBool("RED", true);
                animator.SetBool("BLUE", false);
                Debug.Log("Magnet Activated (RED): " + isActive + ", Attribute: " + magnetAttribute);
            }
            else if (isBlue)
            {
                isActive = true; // アクティブ状態に設定
                animator.SetBool("RED", false);
                animator.SetBool("BLUE", true);
                Debug.Log("Magnet Activated (BLUE): " + isActive + ", Attribute: " + magnetAttribute);
            }
            else
            {
                Debug.Log("Player is not in a valid state for interaction.");
            }
        }
    }

    // マウスが離されたときの処理
    void OnMouseUp()
    {
        isActive = false; // 非アクティブ状態に設定
        animator.SetBool("RED", false);
        animator.SetBool("BLUE", false);
        Debug.Log("Magnet Deactivated: " + isActive + ", Attribute: " + magnetAttribute);
    }
}
