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
        // プレイヤーが何色かによって磁石の属性を変える
        if (playerController.IsRed())
        {
            magnetAttribute = GameManager.MagnetAttribute.Positive;
            gameManager.magnetAttribute = magnetAttribute; // GameManagerに属性変更を反映
            Debug.Log("Magnet Attribute changed to Positive.");
        }
        else if (playerController.IsBlue())
        {
            magnetAttribute = GameManager.MagnetAttribute.Negative;
            gameManager.magnetAttribute = magnetAttribute; // GameManagerに属性変更を反映
            Debug.Log("Magnet Attribute changed to Negative.");
        }

        // ボタン操作で磁石のアクティブ状態を変更
        if (Input.GetKeyDown(KeyCode.Q)) // Qキーで磁石アクティブ
        {
            isActive = true;
            SetMagnetActiveState();
            Debug.Log("Magnet Activated: " + isActive + ", Attribute: " + magnetAttribute);
        }

        if (Input.GetKeyDown(KeyCode.E)) // Eキーで磁石非アクティブ
        {
            isActive = false;
            SetMagnetInactiveState();
            Debug.Log("Magnet Deactivated: " + isActive + ", Attribute: " + magnetAttribute);
        }
    }

    // 磁石がアクティブな状態にする
    private void SetMagnetActiveState()
    {
        animator.SetBool("RED", playerController.IsRed());
        animator.SetBool("BLUE", playerController.IsBlue());
    }

    // 磁石が非アクティブな状態にする
    private void SetMagnetInactiveState()
    {
        animator.SetBool("RED", false);
        animator.SetBool("BLUE", false);
    }
}
