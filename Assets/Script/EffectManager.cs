using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;

    public GameObject lightHitEffect; // �������������ۂ̃G�t�F�N�g
    public GameObject goalEffect;    // �S�[�����B���̃G�t�F�N�g

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
            Destroy(effect, 2f); // �G�t�F�N�g��2�b��ɍ폜
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
