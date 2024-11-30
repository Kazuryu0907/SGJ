using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject magnetObject; // �}�O�l�b�g�I�u�W�F�N�g
    public float repulsionForce = 10f; // �����͂̋���
    public float attractionForce = 10f; // �����񂹗͂̋���
    public float detectionRadius = 5f; // �I�[�o�[���b�v�����o���锼�a�ipublic�ɕύX�j

    public enum MagnetAttribute
    {
        Positive, // ���̑���
        Negative  // ���̑���
    }

    public MagnetAttribute magnetAttribute; // �}�O�l�b�g�I�u�W�F�N�g�̑���

    private void Update()
    {
        // ���a�̕ύX�𔽉f������
        Collider[] colliders = Physics.OverlapSphere(magnetObject.transform.position, detectionRadius); // ���a��detectionRadius�Ŏw��

        foreach (var collider in colliders)
        {
            GameObject target = collider.gameObject;
            if (target.CompareTag("Magnet")) // "Magnet" �^�O�����I�u�W�F�N�g
            {
                // �^�[�Q�b�g�I�u�W�F�N�g�ɑΉ����鑮�����擾
                MagnetAttribute targetAttribute = target.GetComponent<TargetObject>().objectAttribute;

                // ��������v���Ă���Δ����A�Ⴆ�Έ�����
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

    // ��������
    private void Repel(GameObject target)
    {
        Vector3 direction = target.transform.position - magnetObject.transform.position; // �}�O�l�b�g����^�[�Q�b�g�ւ̃x�N�g��
        target.GetComponent<Rigidbody>().AddForce(direction.normalized * repulsionForce); // �����͂�������
    }

    // �����񂹏���
    private void Attract(GameObject target)
    {
        Vector3 direction = magnetObject.transform.position - target.transform.position; // �}�O�l�b�g�ֈ����񂹂�x�N�g��
        target.GetComponent<Rigidbody>().AddForce(direction.normalized * attractionForce); // �����񂹗͂�������
    }

    // ���o���̂��߂�Gizmos���g�p
    private void OnDrawGizmos()
    {
        // �}�O�l�b�g�I�u�W�F�N�g�̈ʒu�𒆐S�ɁA���a�͈͂�\��
        Gizmos.color = Color.green; // �ΐF�ŕ\��
        Gizmos.DrawWireSphere(magnetObject.transform.position, detectionRadius); // ���a�������C���[�t���[���̋���`��
    }
}