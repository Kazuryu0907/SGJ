using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Light spotlight1; // 一つ目の光
    public Light spotlight2; // 二つ目の光
    private Light activeSpotlight; // 現在アクティブな光
    private Camera mainCamera;

    private Renderer playerRenderer; // プレイヤーのレンダラー

    private void Start()
    {
        mainCamera = Camera.main;

        if (spotlight1 == null || spotlight2 == null)
        {
            Debug.LogError("Spotlightが設定されていません。Inspectorで割り当ててください。");
        }

        // 初期状態で最初のライトとマテリアルをアクティブにする
        activeSpotlight = null;
        playerRenderer = GetComponent<Renderer>();
        SetLightVisibility();
    }

    private void Update()
    {
        MoveToMousePosition();
        HandleMouseClick();

        // Qキーでライト1とマテリアル1を選択
        if (Input.GetKeyDown(KeyCode.Q))
        {
            activeSpotlight = spotlight1;
            SetLightVisibility();
        }

        // Wキーでライト2とマテリアル2を選択
        if (Input.GetKeyDown(KeyCode.W))
        {
            activeSpotlight = spotlight2;
            SetLightVisibility();
        }

        // エスケープキーでシーンを切り替える
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Start");
        }
    }

    private void MoveToMousePosition()
    {
        // マウスのスクリーン座標をワールド座標に変換
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(mainCamera.transform.position.z - transform.position.z); // Z距離を考慮

        Vector3 targetPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // プレイヤーをマウス位置に移動（X軸とY軸）
        transform.position = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);
    }

    private void HandleMouseClick()
    {
        // activeSpotlightがnullでない場合にのみ処理を行う
        if (activeSpotlight != null)
        {
            // 左クリックでライトを移動
            if (Input.GetMouseButtonDown(0)) // マウスボタンが押されたとき
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = Mathf.Abs(mainCamera.transform.position.z - activeSpotlight.transform.position.z); // Z距離を調整

                Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

                // ライトの位置をクリックした場所に移動
                activeSpotlight.transform.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);
                activeSpotlight.enabled = true; // ライトを点灯させる
            }

            // マウスボタンが離されたとき
            if (Input.GetMouseButtonUp(0))
            {
                activeSpotlight.enabled = false; // ライトを消す
            }
        }
    }

    private void SetLightVisibility()
    {
        // どちらのライトをアクティブにするかを設定
        spotlight1.enabled = activeSpotlight == spotlight1;
        spotlight2.enabled = activeSpotlight == spotlight2;
    }
}