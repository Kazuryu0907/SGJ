using UnityEngine;

public class Magnet : MonoBehaviour
{
    public bool isActive = false; // ���΂̃A�N�e�B�u���
    public GameManager.MagnetAttribute magnetAttribute; // ���΂̑����i���܂��͕��j

    void Start()
    {
        if (GetComponent<Collider>() == null)
        {
            gameObject.AddComponent<BoxCollider>(); // Collider��������Βǉ�
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
