using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyShotController : MonoBehaviour
{
    public ShotData shotData;
    public float ReloadTime;//�ˌ��Ԋu�̕ϐ�
    public int Damage;
    private Transform player; // �v���C���[��Transform
    public int ShotNumber; //�V���b�g�̎�ނ��w�肷��F1�A�ʏ�e�@2�A�z�[�~���O�e
    public float ShotChance; // �V���b�g�m�� (0.0 �` 1.0)

    private Vector2 moveDirection; // �v���C���[�ւ̈ړ�����
    void Start()
    {
        // �v���C���[�̃I�u�W�F�N�g���擾
        Transform player = GameObject.FindWithTag("Player").transform;

        if (player != null)
        {
            // �v���C���[�̕������v�Z
            Vector2 direction = (player.position - transform.position).normalized;
            moveDirection = direction; // ���ˎ��Ɉړ��������m��
        }
    }

    void Update()
    {
        switch (ShotNumber)
        {
            // ���ʏ�e
            case 0: transform.position += new Vector3(0, shotData.speed, 0) * Time.deltaTime; break;
            // ���z�[�~���O�V���b�g�A�m�肵�������ɒe���ړ�
            case 1: transform.Translate(moveDirection * shotData.speed * Time.deltaTime); break;

        }
        //��ʊO�ɏo����I�u�W�F�N�g������
        if (transform.position.y <= -5)
        {
            Destroy(gameObject);
        }
    }

    // �����ɓ��������Ƃ��̏���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �e�������ɓ���������j�󂷂�
        Destroy(gameObject);
    }
}