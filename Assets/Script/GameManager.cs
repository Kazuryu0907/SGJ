using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject magnetObject; // マグネットオブジェクト
    public float repulsionForce = 10f; // 反発力の強さ
    public float attractionForce = 10f; // 引き寄せ力の強さ
    public float detectionRadius = 5f; // オーバーラップを検出する半径（publicに変更）

    public enum MagnetAttribute
    {
        Positive, // 正の属性
        Negative  // 負の属性
    }

    public MagnetAttribute magnetAttribute; // マグネットオブジェクトの属性

    private void Update()
    {
        // 半径の変更を反映させる
        Collider[] colliders = Physics.OverlapSphere(magnetObject.transform.position, detectionRadius); // 半径をdetectionRadiusで指定

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
        target.GetComponent<Rigidbody>().AddForce(direction.normalized * repulsionForce); // 反発力を加える
    }

    // 引き寄せ処理
    private void Attract(GameObject target)
    {
        Vector3 direction = magnetObject.transform.position - target.transform.position; // マグネットへ引き寄せるベクトル
        target.GetComponent<Rigidbody>().AddForce(direction.normalized * attractionForce); // 引き寄せ力を加える
    }

    // 視覚化のためにGizmosを使用
    private void OnDrawGizmos()
    {
        // マグネットオブジェクトの位置を中心に、半径範囲を表示
        Gizmos.color = Color.green; // 緑色で表示
        Gizmos.DrawWireSphere(magnetObject.transform.position, detectionRadius); // 半径を持つワイヤーフレームの球を描画
    }
}