using UnityEngine;

public class TargetObject : MonoBehaviour
{
    private Animator animator;
    public GameManager.MagnetAttribute objectAttribute; // ターゲットオブジェクトの属性
    private Vector3 prePosition;
    private float scaleSize;
    private float vel = 0;
    public GameObject targetObject;

    private void Start()
    {
        animator = GetComponent<Animator>();
        prePosition = transform.position;
        scaleSize = transform.localScale.x;

        // 初期状態で非表示（必要なら設定）
        if (targetObject != null)
        {
            targetObject.SetActive(false);
        }
    }

    private void Update()
    {
        // 速度計算
        float vel_x = transform.position.x - prePosition.x;
        vel = (transform.position - prePosition).magnitude;

        // Dashアニメーションの設定
        if (vel_x != 0)
        {
            animator.SetBool("dash", true);
        }
        else
        {
            animator.SetBool("dash", false);
        }

        // 向きを設定
        if (vel_x > 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = -scaleSize;
            transform.localScale = scale;
        }
        else
        {
            Vector3 scale = transform.localScale;
            scale.x = scaleSize;
            transform.localScale = scale;
        }

        // targetObject の表示/非表示設定
        if (targetObject != null)
        {
            if (vel < 0.01f) // 停止状態の閾値
            {
                targetObject.SetActive(false);
            }
            else
            {
                targetObject.SetActive(true);
            }
        }

        // 画面内に収める
        ClampToScreen();

        prePosition = transform.position;
    }

    private void ClampToScreen()
    {
        // カメラのビューポート境界を計算
        Camera mainCamera = Camera.main;
        Vector3 position = transform.position;

        Vector3 minScreenBounds = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, position.z - mainCamera.transform.position.z));
        Vector3 maxScreenBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, position.z - mainCamera.transform.position.z));

        // X, Y 座標を制限
        position.x = Mathf.Clamp(position.x, minScreenBounds.x, maxScreenBounds.x);
        position.y = Mathf.Clamp(position.y, minScreenBounds.y, maxScreenBounds.y);

        // 制限された位置を適用
        transform.position = position;
    }
}
