using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioClip lightHitSound;  // 光が当たった時の音
    public AudioClip goalSound;      // ゴール時の音
    public AudioClip gameOverSound;  // ゲームオーバー時の音

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void PlayLightHitSound()
    {
        PlaySound(lightHitSound);
    }

    public void PlayGoalSound()
    {
        PlaySound(goalSound);
    }

    public void PlayGameOverSound()
    {
        PlaySound(gameOverSound);
    }
}
