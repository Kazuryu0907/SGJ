using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] magnetObject; // 磁石オブジェクトのリスト
    public float repulsionForce; // 反発力
    public float attractionForce; // 引力
    public float detectionRadius; // 対象検出範囲

    [SerializeField] private AudioManager audioManager;

    public enum MagnetAttribute
    {
        Positive, // 正の磁石
        Negative  // 負の磁石
    }
    public MagnetAttribute magnetAttribute = MagnetAttribute.Positive; // 初期値はPositive
    private GameObject clickedMagnetObject = null; // クリックされた磁石オブジェクト
    private bool isMagnetSet = false; // 磁石属性が設定されているかどうかを追跡

    //public GameObject player; // プレイヤーオブジェクト
    //public GameObject[] walls;

    private void Update()
    {
        // マウスのクリックを検出
        if (Input.GetMouseButtonDown(0)) // 左クリック
        {
            // クリック位置でRaycastを行い、クリックされたオブジェクトを特定
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            // クリックしたオブジェクトが磁石オブジェクトかどうか確認
            if (hitCollider != null && hitCollider.gameObject.CompareTag("Magnet"))
            {
                // サウンド再生
                audioManager.playBiribiri();
                clickedMagnetObject = hitCollider.gameObject;

                // まだ磁石属性が設定されていない場合のみ属性を設定
                if (!isMagnetSet)
                {
                    // クリックした磁石オブジェクトの属性を保存
                    magnetAttribute = clickedMagnetObject.GetComponent<Magnet>().magnetAttribute;
                    Debug.Log("Magnet Attribute: " + magnetAttribute);
                    // magnetAttribute = MagnetAttribute.Negative;
                    isMagnetSet = true; // 属性が設定されたことを記録
                }
            }else{
                // クリックされたオブジェクトが磁石オブジェクトでない場合、クリックされた磁石オブジェクトをnullに設定
                clickedMagnetObject = null;
            }
        }

        //// プレイヤーと壁の座標を取得
        //Vector2 playerPosition = player.transform.position;

        //foreach (GameObject wall in walls)
        //{
        //    Vector2 wallPosition = wall.transform.position;

        //    if (IsWithinRange(playerPosition, wallPosition, detectionRadius))
        //    {
        //        TriggerExplosion();
        //    }
        //}

        // クリックされた磁石オブジェクトがあり、クリックが押されている間のみ処理を実行
        if (clickedMagnetObject != null && Input.GetMouseButton(0))
        {
            // クリックされた磁石オブジェクトの位置を取得
            Vector3 clickedMagnetPosition = clickedMagnetObject.transform.position;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(clickedMagnetPosition, detectionRadius); // 磁石の周りにいるターゲットを検出

            foreach (var collider in colliders)
            {
                GameObject target = collider.gameObject;

                if (target.CompareTag("Target"))
                {
                    // クリックされた磁石オブジェクトの属性を取得
                    MagnetAttribute targetAttribute = target.GetComponent<TargetObject>().objectAttribute;

                    // 同じ属性なら反発、それ以外は引き寄せ
                    if (magnetAttribute == targetAttribute)
                    {
                        // Debug.Log(123);
                        Repel(target);
                    }
                    else
                    {
                        // Debug.Log(456);
                        Attract(target);
                    }
                }
            }
        }
    }

    // 反発処理
    private void Repel(GameObject target)
    {
        Vector3 direction = target.transform.position - clickedMagnetObject.transform.position; // 磁石からターゲットへの方向ベクトル
        Vector3 targetPosition = clickedMagnetObject.transform.position + direction.normalized * detectionRadius; // 反発先の位置
        // z座標に作用しないように変更
        targetPosition.z = target.transform.position.z;
        // audioManager.playRepel();
        target.transform.position = Vector3.MoveTowards(target.transform.position, targetPosition, repulsionForce * Time.deltaTime); // 反発
    }

    // 引力処理
    private void Attract(GameObject target)
    {
        Vector3 direction = clickedMagnetObject.transform.position - target.transform.position; // 磁石からターゲットへの方向ベクトル
        float distance = direction.magnitude;
        // z座標に作用しないように変更
        Vector3 targetPosition = clickedMagnetObject.transform.position;
        targetPosition.z = target.transform.position.z;
        // ターゲットが磁石から離れていなければ引き寄せ
        if (distance > 0.5f)
        {
            // audioManager.playAttract();
            target.transform.position = Vector3.MoveTowards(target.transform.position, targetPosition, attractionForce * Time.deltaTime); // 引き寄せ
        }
    }

    // Gizmosで磁石の検出範囲を表示
    private void OnDrawGizmos()
    {
        if (magnetObject != null)
        {
            Gizmos.color = Color.green;

            // magnetObject配列内のすべてのオブジェクトに対して検出範囲を表示
            foreach (var magnet in magnetObject)
            {
                Gizmos.DrawWireSphere(magnet.transform.position, detectionRadius);
            }
        }
    }// OnTriggerEnter2Dで衝突を検出し、WallとTargetタグが触れた場合にゲームをリセット
    private void OnTriggerEnter2D(Collider2D other)
    {
        // WallとTargetが触れた場合にシーンをリセット
        if (other.CompareTag("Wall") && clickedMagnetObject != null && clickedMagnetObject.CompareTag("Target"))
        {
            ResetGame();
        }
    }

    //// 判定距離内かを確認する関数
    //private bool IsWithinRange(Vector2 playerPos, Vector2 wallPos, float range)
    //{
    //    return (Mathf.Abs(playerPos.x - wallPos.x) <= range &&
    //            Mathf.Abs(playerPos.y - wallPos.y) <= range);
    //}

    //// 爆散処理
    //private void TriggerExplosion()
    //{
    //    Debug.Log("プレイヤーが壁の範囲内に入りました！爆散処理を実行します！");
    //    // 爆散のアニメーションやエフェクトをここに実装
    //}

    // ゲームをリセット
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
