using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //�ړ��֌W
    const float LOAD_WIDTH = 20f;//�c�����̈ړ��\����
    const float MOVE_MAX_X = 2f;//�������̈ړ��\����
    const float MOVE_MAX_Y = 4.5f;//�������̈ړ��\����
    Vector2 previousPos;//1f�O�̃}�E�X�̈ʒu
    Vector2 currentPos;//���݂̃}�E�X�̈ʒu

    public SplineAnimate splineAnimate; // SplineAnimate���A�^�b�`�����I�u�W�F�N�g��ݒ�
    public bool isSplineFinished = false;

    public GameObject SceneController;

    //�̗͊֌W
    int MaxHP = 100;
    public float HP = 0; // ShotController�Œl����肷�邽��public
    int HeelPoint = 20; // �񕜂����
    int EnemyDamege = 10; // �G�ɐG�ꂽ�Ƃ��̃_���[�W
    float SlipDamege = 0.025f;
    public Image HPbar; // HPBar�̉摜

    //�_���[�W�󂯂��Ƃ��̓_��
    public Renderer PlayerRenderer; //�@�v���C���[�̉摜�f�[�^
    public Color blinkColor = Color.red; // �_�Ŏ��̐F
    float blinkInterval = 0.1f;   // �_�ł̊Ԋu
    int blinkCount = 5; // �_�ł̉�
    bool Hit = false; // �_�Œ����ǂ���
    private Color originalColor; // �v���C���[�̃f�t�H���g�J���[

    

    void Start()
    {
        // ���̐F��ۑ�
        originalColor = PlayerRenderer.material.color;
        HP = MaxHP;
    }

    void Update()
    {
        SceneController sceneController = SceneController.GetComponent<SceneController>();
        if (sceneController.StartGame == true)
        {
            splineAnimate.Play();
            // Spline��̐i�s�x��1.0�ɒB�������ǂ������m�F
            if (!isSplineFinished && splineAnimate.normalizedTime >= 1.0f)
            {
                Debug.Log("Spline�̏I���ɓ��B���܂����I");
                isSplineFinished = true;

                // �ǉ�: Spline�I�����Ƀ^�b�`�ʒu�����Z�b�g
                previousPos = Input.mousePosition;
            }

            // �X���C�v�ɂ��ړ�������Spline�I����̂ݎ��s
            if (isSplineFinished)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    previousPos = Input.mousePosition;
                }

                if (Input.GetMouseButton(0))
                {
                    // �X���C�v�ɂ��ړ��������擾
                    currentPos = Input.mousePosition;
                    float diffDistanceX = (currentPos.x - previousPos.x) / Screen.width * LOAD_WIDTH;
                    float diffDistanceY = (currentPos.y - previousPos.y) / Screen.width * LOAD_WIDTH;

                    // ���̃��[�J��x���W��ݒ� �����̊O�ɂłȂ��悤��
                    float newX = Mathf.Clamp(transform.localPosition.x + diffDistanceX, -MOVE_MAX_X, MOVE_MAX_X);
                    float newY = Mathf.Clamp(transform.localPosition.y + diffDistanceY, -MOVE_MAX_Y, MOVE_MAX_Y);
                    transform.localPosition = new Vector3(newX, newY, 0);

                    // �^�b�v�ʒu���X�V
                    previousPos = currentPos;
                }
            }

            if (HP <= 0)
            {
                Debug.Log("aaa");
                Destroy(gameObject);
            }

            //HP�̍X�V
            UpdateHPBar();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //���@���_�ł��Ă��Ȃ��Ƃ��̂݃_���[�W����
        if ((Hit == false))
        {
            //�G�{��
            if (collision.tag == "Enemy")
            {
                Debug.Log("Enemy");
                HP -= EnemyDamege;
                StartCoroutine(Blink()); // �_�ł̌Ăяo��
            }
            //�G�̒e
            if (collision.tag == "EnemyShot")
            {
                EnemyShotController EnemyShot = collision.GetComponent<EnemyShotController>();
                Destroy(collision.gameObject);
                Debug.Log("Shot");
                HP -= EnemyShot.Damage;
                StartCoroutine(Blink());
            }
        }
        //�񕜏���
        if (collision.tag == "HeelItem")
        {
            Destroy(collision.gameObject);
            HP += HeelPoint;
            if(HP >= MaxHP)
            {
                HP = MaxHP;
            }
            Destroy(collision.gameObject);
        }
        UpdateHPBar();
    }

    //HP�̍X�V����
    void UpdateHPBar()
    {
        float fillAmount = HP / MaxHP; // HP�̊������v�Z
        HPbar.fillAmount = fillAmount;   // HP�o�[��fillAmount�ɔ��f
    }

    //�_�Ŏ��̏���
    IEnumerator Blink()
    {
        Hit = true;
        for(int i = 1; i<= blinkCount; i++)
        {
            // �I�u�W�F�N�g�̐F��ύX
            PlayerRenderer.material.color = blinkColor;
            yield return new WaitForSeconds(blinkInterval);

            // ���̐F�ɖ߂�
            PlayerRenderer.material.color = originalColor;
            yield return new WaitForSeconds(blinkInterval);
        }
        Hit = false;
    }
}