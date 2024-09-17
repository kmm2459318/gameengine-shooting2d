using UnityEngine;
using UnityEngine.Splines;

public class EnemyController : MonoBehaviour
{
    public EnemyData EnemyData;
    int HP;

    public SplineAnimate splineAnimate; // SplineAnimate���A�^�b�`�����I�u�W�F�N�g��ݒ�

    float speed; // �ړ����x
    private Vector3 startPosition;
    private Vector3 endPosition; // �ړ��͈͂̏I�_
    private bool movingRight = true; // �ŏ�����E�����ɓ���
    public bool hasReachedEnd = false;

    void Start()
    {
        HP = EnemyData.HP;
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

    void Update()
    {
        Vector3 currentPosition = transform.position;

        if ((splineAnimate == true))
        {
            // Spline��̐i�s�x��1.0�ɒB�������ǂ������m�F
            if (splineAnimate.normalizedTime >= 1.0f && hasReachedEnd == false)
            {
                Debug.Log("Spline�̏I���ɓ��B���܂����I");
                // �I�����̏����������ɒǉ�
                hasReachedEnd = true; // �ړ��I��
            }
        }
       

        //���ړ��̏���
        if (movingRight)
        {
            currentPosition.x += speed * Time.deltaTime;

            // �I�_�ɒB����������𔽓]
            if (currentPosition.x >= endPosition.x)
            {
                movingRight = false;
            }
        }
        else
        {
            currentPosition.x -= speed * Time.deltaTime;

            // �N�_�ɒB����������𔽓]
            if (currentPosition.x <= startPosition.x)
            {
                movingRight = true;
            }
        }

        transform.position = currentPosition;

        if (HP <= 0)
        {
            DropItem();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Shot")
        {
            ShotController shotController = collision.GetComponent<ShotController>();
            if (shotController != null)
            {
                HP -= shotController.Damage;
                Destroy(collision.gameObject);
                Debug.Log("Enemy hit! Remaining HP: " + HP);
            }
        }
        if (collision.tag == "Player")
        {
            Debug.Log("Player");
        }
    }

    private void DropItem()
    {
        if (EnemyData.dropItemPrefab != null && Random.value < EnemyData.dropChance)
        {
            int DropItems = EnemyData.dropItemPrefab.Length;
            Instantiate(EnemyData.dropItemPrefab[Random.Range(0,DropItems)], transform.position, Quaternion.identity);
        }
    }
}
