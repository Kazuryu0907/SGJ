using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;

    public GameObject lightHitEffect; // 光が当たった際のエフェクト
    public GameObject goalEffect;    // ゴール到達時のエフェクト

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayEffect(Vector3 position, GameObject effectPrefab)
    {
        if (effectPrefab != null)
        {
            GameObject effect = Instantiate(effectPrefab, position, Quaternion.identity);
            Destroy(effect, 2f); // エフェクトを2秒後に削除
        }
    }

    public void PlayLightHitEffect(Vector3 position)
    {
        PlayEffect(position, lightHitEffect);
    }

    public void PlayGoalEffect(Vector3 position)
    {
        PlayEffect(position, goalEffect);
    }
}
