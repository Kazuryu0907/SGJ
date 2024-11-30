using UnityEngine;
using UnityEngine.UIElements;

public class TargetObject : MonoBehaviour
{
    private Animator animator;
    public GameManager.MagnetAttribute objectAttribute; // �^�[�Q�b�g�I�u�W�F�N�g�̑���
    private Vector3 prePosition;
    private float scaleSize;
    private float vel = 0;

    private void Start(){
        animator = GetComponent<Animator>();
        prePosition = transform.position;
        scaleSize = transform.localScale.x;
    }
    private void Update(){
        // 右に動いてたら
        float vel_x = transform.position.x - prePosition.x;
        vel = (transform.position - prePosition).magnitude;
        // DashのAnimation
        if(vel_x != 0) animator.SetBool("dash", true);
        else animator.SetBool("dash", false);
        
        if(vel_x > 0){
            Vector3 scale = transform.localScale;
            scale.x = -scaleSize;
            transform.localScale = scale;
        }else{
            Vector3 scale = transform.localScale;
            scale.x = scaleSize;
            transform.localScale = scale;
        }

        prePosition = transform.position;
    }
}
