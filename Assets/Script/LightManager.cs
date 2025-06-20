using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public Light spotLight;
    public float maxLightDuration = 10f; // 光の上限時間
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
                spotLight.transform.position = hit.point + Vector3.up * 2f; // 光源を地面から少し浮かせる
                currentLightDuration -= Time.deltaTime;
                if (currentLightDuration <= 0)
                {
                    //GameManager.Instance.GameOver();
                }
            }
        }
        if(Input.GetButtonDown("Q"))
        {

        }
        //else
        //{
        //    RecoverLight();
        //}
    }

    //private void RecoverLight()
    //{
    //    currentLightDuration = Mathf.Min(maxLightDuration, currentLightDuration + Time.deltaTime * 0.5f); // ゆっくり回復
    //}
}