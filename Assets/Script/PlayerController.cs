using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Light spotlight1; // ��ڂ̌�
    public Light spotlight2; // ��ڂ̌�
    public Image red;
    public Image blue;

    private Light activeSpotlight; // ���݃A�N�e�B�u�Ȍ�
    private Camera mainCamera;
    private Renderer playerRenderer; // �v���C���[�̃����_���[
    private GameObject clickedMagnetObject = null; // �N���b�N���ꂽ�}�O�l�b�g�I�u�W�F�N�g

    private Animator animator; // Animator�R���|�[�l���g

    private void Start()
    {
        mainCamera = Camera.main;
        animator = GetComponent<Animator>(); // Animator�R���|�[�l���g���擾

        if (spotlight1 == null || spotlight2 == null)
        {
            Debug.LogError("Spotlight���ݒ肳��Ă��܂���BInspector�Ŋ��蓖�ĂĂ��������B");
        }
        if (animator == null)
        {
            Debug.LogError("Animator���A�^�b�`����Ă��܂���BInspector�Ŋ��蓖�ĂĂ��������B");
        }

        red.enabled = true;
        blue.enabled = false;
        activeSpotlight = spotlight1;
        spotlight2.enabled = false;
        playerRenderer = GetComponent<Renderer>();
        SetLightVisibility();

        // ������Ԃ̃A�j���[�V�����ݒ�
        animator.SetBool("RED", true);
        animator.SetBool("BLUE", false);
    }

    private void Update()
    {
        MoveToMousePosition();
        HandleMouseClick();

        // Q�L�[�Ń��C�g1��I�����ARED�A�j���[�V������L����
        if (Input.GetKeyDown(KeyCode.Q))
        {
            activeSpotlight = spotlight1;
            SetLightVisibility();
            red.enabled = true;
            blue.enabled = false;

            animator.SetBool("RED", true);
            animator.SetBool("BLUE", false);
        }

        // W�L�[�Ń��C�g2��I�����ABLUE�A�j���[�V������L����
        if (Input.GetKeyDown(KeyCode.W))
        {
            activeSpotlight = spotlight2;
            SetLightVisibility();
            red.enabled = false;
            blue.enabled = true;

            animator.SetBool("RED", false);
            animator.SetBool("BLUE", true);
        }

        // �G�X�P�[�v�L�[�ŃV�[����؂�ւ���
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("StageSelection");
        }
    }

    private void MoveToMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);

        Vector3 targetPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        float screenWidth = mainCamera.orthographicSize * 2 * Screen.width / Screen.height;
        float screenHeight = mainCamera.orthographicSize * 2;

        float clampedX = Mathf.Clamp(targetPosition.x, -screenWidth / 2, screenWidth / 2);
        float clampedY = Mathf.Clamp(targetPosition.y, -screenHeight / 2, screenHeight / 2);

        transform.position = new Vector3(clampedX, clampedY, targetPosition.z);
    }
    public bool IsRed()
    {
        return animator.GetBool("RED");
    }

    public bool IsBlue()
    {
        return animator.GetBool("BLUE");
    }

    private void HandleMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            if (hitCollider != null && hitCollider.gameObject.CompareTag("Magnet"))
            {
                clickedMagnetObject = hitCollider.gameObject;

                Vector3 magnetPosition = clickedMagnetObject.transform.position;
                transform.position = new Vector3(magnetPosition.x, magnetPosition.y, transform.position.z);

                activeSpotlight.transform.position = new Vector3(magnetPosition.x, magnetPosition.y, activeSpotlight.transform.position.z);
            }
        }

        if (activeSpotlight != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = Mathf.Abs(mainCamera.transform.position.z - activeSpotlight.transform.position.z);

                Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

                activeSpotlight.transform.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);
                activeSpotlight.enabled = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                activeSpotlight.enabled = false;
            }
        }
    }

    private void SetLightVisibility()
    {
        spotlight1.enabled = activeSpotlight == spotlight1;
        spotlight2.enabled = activeSpotlight == spotlight2;
    }
}