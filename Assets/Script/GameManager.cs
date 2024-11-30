using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject magnetObject; // マグネットオブジェクト
    public float repulsionForce; // 反発力の強さ
    public float attractionForce; // 引き寄せ力の強さ
    public float detectionRadius; // オーバーラップを検出する半径

    public enum MagnetAttribute
    {
        Positive, // 正の属性
        Negative  // 負の属性
    }

    public MagnetAttribute magnetAttribute; // マグネットオブジェクトの属性

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(magnetObject.transform.position, detectionRadius); // 半径を指定

        foreach (var collider in colliders)
        {
            GameObject target = collider.gameObject;
            if (target.CompareTag("Magnet")) // "Magnet" タグを持つオブジェクト
            {
                // ターゲットオブジェクトに対応する属性を取得
                MagnetAttribute targetAttribute = target.GetComponent<TargetObject>().objectAttribute;

                // 属性が一致していれば反発、違えば引き寄せ
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

    // 反発処理
    private void Repel(GameObject target)
    {
        Vector3 direction = target.transform.position - magnetObject.transform.position; // マグネットからターゲットへのベクトル
        Vector3 targetPosition = magnetObject.transform.position + direction.normalized * detectionRadius; // 境界付近の位置
        target.transform.position = Vector3.MoveTowards(target.transform.position, targetPosition, repulsionForce * Time.deltaTime); // 境界に向かう
    }

    // 引き寄せ処理
    private void Attract(GameObject target)
    {
        Vector3 direction = magnetObject.transform.position - target.transform.position; // マグネットへ引き寄せるベクトル
        float distance = direction.magnitude;

        // マグネットにぶつからないようにする
        if (distance > 0.5f) // 距離が0.5未満にならないよう制限
        {
            target.transform.position = Vector3.MoveTowards(target.transform.position, magnetObject.transform.position, attractionForce * Time.deltaTime);
        }
    }

    // 視覚化のためにGizmosを使用
    private void OnDrawGizmos()
    {
        if (magnetObject != null)
        {
            Gizmos.color = Color.green; // 緑色で表示
            Gizmos.DrawWireSphere(magnetObject.transform.position, detectionRadius); // 半径を持つワイヤーフレームの球を描画
        }
    }
}