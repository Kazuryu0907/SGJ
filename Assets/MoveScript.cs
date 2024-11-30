using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W)){
            transform.position += new Vector3(0, 1f, 0) * Time.deltaTime;
        }else if(Input.GetKey(KeyCode.S)){
            transform.position += new Vector3(0, -1f, 0) * Time.deltaTime;
        }else if(Input.GetKey(KeyCode.A)){
            transform.position += new Vector3(-1f, 0, 0) * Time.deltaTime;
        }else if(Input.GetKey(KeyCode.D)){
            transform.position += new Vector3(1f, 0, 0) * Time.deltaTime;
        }
    }

    // private void OnTriggerEnter2D(Collider2D other){
    //     Debug.Log("1.OnTriggerEnter");
    // }
    // private void OnCollisionEnter2D(Collision2D other){
    //     Debug.Log("1.OnCollisionEnter");
    // }
}
