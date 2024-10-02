using UnityEngine;
using UnityEngine.Splines;

public class EnemyController : MonoBehaviour
{

    public EnemyData EnemyData;
    public int HP;
    public int MaxHP;

    public SplineAnimate splineAnimate; // SplineAnimate���A�^�b�`�����I�u�W�F�N�g��ݒ�

    public float speed; // �ړ����x
    public Vector3 startPosition;
    public Vector3 endPosition; // �ړ��͈͂̏I�_
    public bool movingRight = true; // �ŏ�����E�����ɓ���
    public bool hasReachedEnd = false;


    void Start()
    {
        HP = EnemyData.HP;
        MaxHP = EnemyData.HP;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        speed = EnemyData.speed;

        // ��ʒ����̈ʒu���擾
        Vector3 screenCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
        screenCenter.z = 0; // Z���̈ʒu�͕s�v�Ȃ̂�0�ɐݒ�

        // �ړ��͈͂̐ݒ�
        startPosition = new Vector3(screenCenter.x - EnemyData.moveDistance / 2, transform.position.y, transform.position.z);
        endPosition = new Vector3(screenCenter.x + EnemyData.moveDistance / 2, transform.position.y, transform.position.z);

        // �����ʒu��ݒ�
        //transform.position = startPosition;

        // movingRight �� true �̂܂܁A�E�����ɍŏ����瓮����
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�K�E�Z�A�ђʃV���b�g�Ȃ̂ŏ����Ȃ�
        if (collision.tag == "HyperShot")
        {
            Damege(collision);
        }
        //�ʏ�V���b�g�A�ђʂ��Ȃ��̂ŏ���
        if (collision.tag == "Shot")
        {
            Damege(collision);
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Player")
        {
            Debug.Log("Player");
        }
    }

    //�A�C�e��drop�̏���
    private void DropItem()
    {
        if (Random.value < EnemyData.DropChance)
            if (EnemyData.dropItemData != null && EnemyData.dropItemData.Length > 0)
            {
                foreach (var itemData in EnemyData.dropItemData)
                {
                    // dropChance���g���ăA�C�e�����h���b�v����邩�ǂ����𔻒�
                    if (Random.value < itemData.dropChance)
                    {
                        Instantiate(itemData.prefab, transform.position, Quaternion.identity);
                        break; // 1�̃A�C�e�����h���b�v������I��
                    }
                }
            }
    }

    //�_���[�W�̏���
    private void Damege(Collider2D collision)
    {
        ShotController shotController = collision.GetComponent<ShotController>();
        if (shotController != null)
        {
            HP -= shotController.Damage;
            Debug.Log("Enemy hit! Remaining HP: " + HP);
            if (HP <= 0)
            {
                DropItem();
                Destroy(gameObject);
            }
        }
    }
}
