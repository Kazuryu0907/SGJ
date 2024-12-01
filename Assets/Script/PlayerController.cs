using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Light spotlight1; // 一つ目の光
    public Light spotlight2; // 二つ目の光
    public Image red;
    public Image blue;

    private Light activeSpotlight; // 現在アクティブな光
    private Camera mainCamera;
    private Renderer playerRenderer; // プレイヤーのレンダラー
    private GameObject clickedMagnetObject = null; // クリックされたマグネットオブジェクト

    private Animator animator; // Animatorコンポーネント

    private void Start()
    {
        mainCamera = Camera.main;
        animator = GetComponent<Animator>(); // Animatorコンポーネントを取得

        if (spotlight1 == null || spotlight2 == null)
        {
            Debug.LogError("Spotlightが設定されていません。Inspectorで割り当ててください。");
        }
        if (animator == null)
        {
            Debug.LogError("Animatorがアタッチされていません。Inspectorで割り当ててください。");
        }

        red.enabled = true;
        blue.enabled = false;
        activeSpotlight = spotlight1;
        spotlight2.enabled = false;
        playerRenderer = GetComponent<Renderer>();
        SetLightVisibility();

        // 初期状態のアニメーション設定
        animator.SetBool("RED", true);
        animator.SetBool("BLUE", false);
    }

    private void Update()
    {
        MoveToMousePosition();
        HandleMouseClick();

        // Qキーでライト1を選択し、REDアニメーションを有効化
        if (Input.GetKeyDown(KeyCode.Q))
        {
            activeSpotlight = spotlight1;
            SetLightVisibility();
            red.enabled = true;
            blue.enabled = false;

            animator.SetBool("RED", true);
            animator.SetBool("BLUE", false);
        }

        // Wキーでライト2を選択し、BLUEアニメーションを有効化
        if (Input.GetKeyDown(KeyCode.W))
        {
            activeSpotlight = spotlight2;
            SetLightVisibility();
            red.enabled = false;
            blue.enabled = true;

            animator.SetBool("RED", false);
            animator.SetBool("BLUE", true);
        }

        // エスケープキーでシーンを切り替える
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("StageSelection");
        }
    }

    private void MoveToMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);

        Vector3 targetPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        float screenWidth = mainCamera.orthographicSize * 2 * Screen.width / Screen.height;
        float screenHeight = mainCamera.orthographicSize * 2;

        float clampedX = Mathf.Clamp(targetPosition.x, -screenWidth / 2, screenWidth / 2);
        float clampedY = Mathf.Clamp(targetPosition.y, -screenHeight / 2, screenHeight / 2);

        transform.position = new Vector3(clampedX, clampedY, targetPosition.z);
    }
    public bool IsRed()
    {
        return animator.GetBool("RED");
    }

    public bool IsBlue()
    {
        return animator.GetBool("BLUE");
    }

    private void HandleMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            if (hitCollider != null && hitCollider.gameObject.CompareTag("Magnet"))
            {
                clickedMagnetObject = hitCollider.gameObject;

                Vector3 magnetPosition = clickedMagnetObject.transform.position;
                transform.position = new Vector3(magnetPosition.x, magnetPosition.y, transform.position.z);

                activeSpotlight.transform.position = new Vector3(magnetPosition.x, magnetPosition.y, activeSpotlight.transform.position.z);
            }
        }

        if (activeSpotlight != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = Mathf.Abs(mainCamera.transform.position.z - activeSpotlight.transform.position.z);

                Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

                activeSpotlight.transform.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);
                activeSpotlight.enabled = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                activeSpotlight.enabled = false;
            }
        }
    }

    private void SetLightVisibility()
    {
        spotlight1.enabled = activeSpotlight == spotlight1;
        spotlight2.enabled = activeSpotlight == spotlight2;
    }
}