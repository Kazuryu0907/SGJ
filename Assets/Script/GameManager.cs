using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] magnetObject; // 磁石オブジェクトのリスト
    public float repulsionForce; // 反発力
    public float attractionForce; // 引力
    public float detectionRadius; // 対象検出範囲

    public enum MagnetAttribute
    {
        Positive, // 正の磁石
        Negative  // 負の磁石
    }
    public MagnetAttribute magnetAttribute = MagnetAttribute.Positive; // 初期値はPositive
    private GameObject clickedMagnetObject = null; // クリックされた磁石オブジェクト
    private bool isMagnetSet = false; // 磁石属性が設定されているかどうかを追跡

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
                clickedMagnetObject = hitCollider.gameObject;

                // まだ磁石属性が設定されていない場合のみ属性を設定
                if (!isMagnetSet)
                {
                    // クリックした磁石オブジェクトの属性を保存
                    magnetAttribute = clickedMagnetObject.GetComponent<Magnet>().magnetAttribute;
                    isMagnetSet = true; // 属性が設定されたことを記録
                }
            }
        }

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
                        Repel(target);
                    }
                    else
                    {
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
        target.transform.position = Vector3.MoveTowards(target.transform.position, targetPosition, repulsionForce * Time.deltaTime); // 反発
    }

    // 引力処理
    private void Attract(GameObject target)
    {
        Vector3 direction = clickedMagnetObject.transform.position - target.transform.position; // 磁石からターゲットへの方向ベクトル
        float distance = direction.magnitude;

        // ターゲットが磁石から離れていなければ引き寄せ
        if (distance > 0.5f)
        {
            target.transform.position = Vector3.MoveTowards(target.transform.position, clickedMagnetObject.transform.position, attractionForce * Time.deltaTime); // 引き寄せ
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
    }
}
