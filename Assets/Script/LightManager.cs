using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public Light spotLight;
    public float maxLightDuration = 10f; // ���̏������
    private float currentLightDuration;

    private void Start()
    {
        currentLightDuration = maxLightDuration;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                spotLight.transform.position = hit.point + Vector3.up * 2f; // ������n�ʂ��班����������
                currentLightDuration -= Time.deltaTime;
                if (currentLightDuration <= 0)
                {
                    GameManager.Instance.GameOver();
                }
            }
        }
        else
        {
            RecoverLight();
        }
    }

    private void RecoverLight()
    {
        currentLightDuration = Mathf.Min(maxLightDuration, currentLightDuration + Time.deltaTime * 0.5f); // ��������
    }
}