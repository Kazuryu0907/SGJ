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
            // 速度がほぼ0なら非表示、それ以外は表示
            if (vel < 0.01f) // 0.01f は閾値（微調整可能）
            {
                targetObject.SetActive(false);
            }
            else
            {
                targetObject.SetActive(true);
            }
        }

        prePosition = transform.position;
    }
}
