using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject magnetObject; // �}�O�l�b�g�I�u�W�F�N�g
    public float repulsionForce; // �����͂̋���
    public float attractionForce; // �����񂹗͂̋���
    public float detectionRadius; // �I�[�o�[���b�v�����o���锼�a

    public enum MagnetAttribute
    {
        Positive, // ���̑���
        Negative  // ���̑���
    }

    public MagnetAttribute magnetAttribute; // �}�O�l�b�g�I�u�W�F�N�g�̑���

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(magnetObject.transform.position, detectionRadius); // ���a���w��

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
        Vector3 targetPosition = magnetObject.transform.position + direction.normalized * detectionRadius; // ���E�t�߂̈ʒu
        target.transform.position = Vector3.MoveTowards(target.transform.position, targetPosition, repulsionForce * Time.deltaTime); // ���E�Ɍ�����
    }

    // �����񂹏���
    private void Attract(GameObject target)
    {
        Vector3 direction = magnetObject.transform.position - target.transform.position; // �}�O�l�b�g�ֈ����񂹂�x�N�g��
        float distance = direction.magnitude;

        // �}�O�l�b�g�ɂԂ���Ȃ��悤�ɂ���
        if (distance > 0.5f) // ������0.5�����ɂȂ�Ȃ��悤����
        {
            target.transform.position = Vector3.MoveTowards(target.transform.position, magnetObject.transform.position, attractionForce * Time.deltaTime);
        }
    }

    // ���o���̂��߂�Gizmos���g�p
    private void OnDrawGizmos()
    {
        if (magnetObject != null)
        {
            Gizmos.color = Color.green; // �ΐF�ŕ\��
            Gizmos.DrawWireSphere(magnetObject.transform.position, detectionRadius); // ���a�������C���[�t���[���̋���`��
        }
    }
}