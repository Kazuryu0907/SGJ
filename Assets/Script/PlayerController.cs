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

    private void Start()
    {
        mainCamera = Camera.main;

        if (spotlight1 == null || spotlight2 == null)
        {
            Debug.LogError("Spotlight���ݒ肳��Ă��܂���BInspector�Ŋ��蓖�ĂĂ��������B");
        }
        red.enabled = true;
        blue.enabled = false;
        // ������Ԃōŏ��̃��C�g�ƃ}�e���A�����A�N�e�B�u�ɂ���
        activeSpotlight = spotlight1;
        spotlight2.enabled = false;
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
            red.enabled = true;
            blue.enabled = false;
        }

        // W�L�[�Ń��C�g2�ƃ}�e���A��2��I��
        if (Input.GetKeyDown(KeyCode.W))
        {
            activeSpotlight = spotlight2;
            SetLightVisibility();
            red.enabled = false;
            blue.enabled = true;
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
        // �}�E�X�N���b�N�����o���āA�N���b�N�����}�O�l�b�g�I�u�W�F�N�g�Ƀv���C���[���ړ�
        if (Input.GetMouseButtonDown(0)) // ���N���b�N
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            // �N���b�N�����I�u�W�F�N�g���}�O�l�b�g���ǂ����m�F
            if (hitCollider != null && hitCollider.gameObject.CompareTag("Magnet"))
            {
                clickedMagnetObject = hitCollider.gameObject;

                // �v���C���[���N���b�N�����}�O�l�b�g�̒��S�Ɉړ�
                Vector3 magnetPosition = clickedMagnetObject.transform.position;
                transform.position = new Vector3(magnetPosition.x, magnetPosition.y, transform.position.z);

                // ���C�g�̈ʒu���}�O�l�b�g�ɍ��킹��
                activeSpotlight.transform.position = new Vector3(magnetPosition.x, magnetPosition.y, activeSpotlight.transform.position.z);
            }
        }

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