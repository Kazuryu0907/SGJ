using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public Light spotLight;
    public float maxLightDuration = 10f; // Œõ‚ÌãŒÀŠÔ
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
                spotLight.transform.position = hit.point + Vector3.up * 2f; // ŒõŒ¹‚ğ’n–Ê‚©‚ç­‚µ•‚‚©‚¹‚é
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
        currentLightDuration = Mathf.Min(maxLightDuration, currentLightDuration + Time.deltaTime * 0.5f); // ‚ä‚Á‚­‚è‰ñ•œ
    }
}