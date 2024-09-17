using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    const float LOAD_WIDTH = 20f;//�c�����̈ړ��\����
    const float MOVE_MAX_X = 2f;//�������̈ړ��\����
    const float MOVE_MAX_Y = 4.5f;//�������̈ړ��\����
    Vector2 previousPos;//1f�O�̃}�E�X�̈ʒu
    Vector2 currentPos;//���݂̃}�E�X�̈ʒu
    int MaxHP = 100;
    float HP = 0;

    public Image HPbar;

    void Start()
    {
        HP = MaxHP;
    }
    void Update()
    {
        // �X���C�v�ɂ��ړ�����
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


        if (HP <= 0)
        {
            Debug.Log("aaa");
            Destroy(gameObject);
        }

        //����HP�����
        HP -= 0.03f;
        //HP�̍X�V
        UpdateHPBar();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Debug.Log("Enemy");
            HP -= 10;
        }
        if (collision.tag == "EnemyShot")
        {
            EnemyShotController EnemyShot = collision.GetComponent<EnemyShotController>();
            Destroy(collision.gameObject);
            Debug.Log("Shot");
            HP -= EnemyShot.Damage;
        }
        if (collision.tag == "HeelItem")
        {
            HP += 50;
            if(HP >= MaxHP)
            {
                HP = MaxHP;
            }
        }
        UpdateHPBar();
    }

    //HP�̍X�V����
    void UpdateHPBar()
    {
        float fillAmount = HP / MaxHP; // HP�̊������v�Z
        HPbar.fillAmount = fillAmount;   // HP�o�[��fillAmount�ɔ��f
    }
}