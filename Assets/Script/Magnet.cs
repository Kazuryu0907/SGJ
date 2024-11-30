using UnityEngine;

public class Magnet : MonoBehaviour
{
    public bool isActive = false; // ���΂̃A�N�e�B�u���
    public GameManager.MagnetAttribute magnetAttribute; // ���΂̑����i���܂��͕��j
    private GameManager gameManager; // GameManager�̎Q��

    void Start()
    {
        // GameManager�̎Q�Ƃ��擾
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager���V�[���Ɍ�����܂���I");
        }

        if (GetComponent<Collider2D>() == null)
        {
            gameObject.AddComponent<BoxCollider2D>(); // Collider��������Βǉ�
        }
    }

    void Update()
    {
        // Q�L�[�Ŏ��Α����𐳂ɐݒ�
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (gameManager != null)
            {
                gameManager.magnetAttribute = GameManager.MagnetAttribute.Positive;
                Debug.Log("Magnet Attribute changed to Positive.");
            }
            else
            {
                Debug.LogWarning("gameManager is null");
            }
        }

        // W�L�[�Ŏ��Α����𕉂ɐݒ�
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (gameManager != null)
            {
                gameManager.magnetAttribute = GameManager.MagnetAttribute.Negative;
                Debug.Log("Magnet Attribute changed to Negative.");
            }
            else
            {
                Debug.LogWarning("gameManager is null");
            }
        }
    }

    // �}�E�X���������Ƃ��ɃA�N�e�B�u�ɂ���
    void OnMouseDown()
    {
        isActive = true; // �A�N�e�B�u��Ԃɂ���
        Debug.Log("Magnet Activated: " + isActive + ", Attribute: " + magnetAttribute);
    }

    // �}�E�X�𗣂����Ƃ��ɔ�A�N�e�B�u�ɂ���
    void OnMouseUp()
    {
        isActive = false; // ��A�N�e�B�u��Ԃɂ���
        Debug.Log("Magnet Deactivated: " + isActive + ", Attribute: " + magnetAttribute);
    }
}
