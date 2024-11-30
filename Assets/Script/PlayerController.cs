using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Light spotlight1; // ��ڂ̌�
    public Light spotlight2; // ��ڂ̌�
    private Light activeSpotlight; // ���݃A�N�e�B�u�Ȍ�
    private Camera mainCamera;

    private Renderer playerRenderer; // �v���C���[�̃����_���[

    private void Start()
    {
        mainCamera = Camera.main;

        if (spotlight1 == null || spotlight2 == null)
        {
            Debug.LogError("Spotlight���ݒ肳��Ă��܂���BInspector�Ŋ��蓖�ĂĂ��������B");
        }

        // ������Ԃōŏ��̃��C�g�ƃ}�e���A�����A�N�e�B�u�ɂ���
        activeSpotlight = null;
        playerRenderer = GetComponent<Renderer>();
        SetLightVisibility();
    }

    private void Update()
    {
        MoveToMousePosition();
        HandleMouseClick();

        // Q�L�[�Ń��C�g1�ƃ}�e���A��1��I��
        if (Input.GetKeyDown(KeyCode.Q))
        {
            activeSpotlight = spotlight1;
            SetLightVisibility();
        }

        // W�L�[�Ń��C�g2�ƃ}�e���A��2��I��
        if (Input.GetKeyDown(KeyCode.W))
        {
            activeSpotlight = spotlight2;
            SetLightVisibility();
        }

        // �G�X�P�[�v�L�[�ŃV�[����؂�ւ���
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Start");
        }
    }

    private void MoveToMousePosition()
    {
        // �}�E�X�̃X�N���[�����W�����[���h���W�ɕϊ�
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(mainCamera.transform.position.z - transform.position.z); // Z�������l��

        Vector3 targetPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // �v���C���[���}�E�X�ʒu�Ɉړ��iX����Y���j
        transform.position = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);
    }

    private void HandleMouseClick()
    {
        // activeSpotlight��null�łȂ��ꍇ�ɂ̂ݏ������s��
        if (activeSpotlight != null)
        {
            // ���N���b�N�Ń��C�g���ړ�
            if (Input.GetMouseButtonDown(0)) // �}�E�X�{�^���������ꂽ�Ƃ�
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = Mathf.Abs(mainCamera.transform.position.z - activeSpotlight.transform.position.z); // Z�����𒲐�

                Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

                // ���C�g�̈ʒu���N���b�N�����ꏊ�Ɉړ�
                activeSpotlight.transform.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);
                activeSpotlight.enabled = true; // ���C�g��_��������
            }

            // �}�E�X�{�^���������ꂽ�Ƃ�
            if (Input.GetMouseButtonUp(0))
            {
                activeSpotlight.enabled = false; // ���C�g������
            }
        }
    }

    private void SetLightVisibility()
    {
        // �ǂ���̃��C�g���A�N�e�B�u�ɂ��邩��ݒ�
        spotlight1.enabled = activeSpotlight == spotlight1;
        spotlight2.enabled = activeSpotlight == spotlight2;
    }
}