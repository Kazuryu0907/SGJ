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


    private void Update()
    {
        // Collider[] colliders = Physics.OverlapSphere(magnetObject.transform.position, detectionRadius); // ���a���w��
        Collider2D[] colliders = Physics2D.OverlapCircleAll(magnetObject.transform.position, detectionRadius); // ���a���w��

        foreach (var collider in colliders)
        {
            GameObject target = collider.gameObject;
            MagnetAttribute magnetAttribute = magnetObject.GetComponent<Magnet>().magnetAttribute;;
            if (target.CompareTag("Target")) // "Magnet" �^�O�����I�u�W�F�N�g
            {
                // �^�[�Q�b�g�I�u�W�F�N�g�ɑΉ����鑮�����擾
                MagnetAttribute targetAttribute = target.GetComponent<TargetObject>().objectAttribute;
                Debug.Log("target:"+targetAttribute);
                Debug.Log("magnet:"+magnetAttribute);
                // ��������v���Ă���Δ����A�Ⴆ�Έ�����
                if (magnetAttribute == targetAttribute)
                {
                    // Debug.Log("Repelling");
                    Repel(target);
                }
                else
                {
                    // Debug.Log("Attracting");
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
        Debug.Log(direction);
        Debug.Log(targetPosition);
        // target.transform.position.
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