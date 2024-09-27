using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class SlideMove : MonoBehaviour
{
    public EnemyController EnemyPrefab;

    //�{�[�i�X�G�l�~�[����莞�Ԍ�ɏ������߂�
    public bool BonusEnemy = false;
    int BoundCount = 0;
    public int BoundLimit = 5;

    public float flySpeed = 1f;   // ��ɔ�ԃX�s�[�h
    public float destroyTime = 5f; // ������܂ł̎���
    void Update()
    {
        Vector3 currentPosition = transform.position;

        if ((EnemyPrefab.splineAnimate == true))
        {
;            // Spline��̐i�s�x��1.0�ɒB�������ǂ������m�F
            if (EnemyPrefab.splineAnimate.normalizedTime >= 1.0f && EnemyPrefab.hasReachedEnd == false)
            {
                Debug.Log("Spline�̏I���ɓ��B���܂����I");
                // �I�����̏����������ɒǉ�
                EnemyPrefab.hasReachedEnd = true; // �ړ��I��
            }
        }


        //���ړ��̏���
        if (EnemyPrefab.movingRight)
        {
            currentPosition.x += EnemyPrefab.speed * Time.deltaTime;

            // �I�_�ɒB����������𔽓]
            if (currentPosition.x >= EnemyPrefab.endPosition.x)
            {
                EnemyPrefab.movingRight = false;
                BoundCount++;
            }
        }
        else
        {
            currentPosition.x -= EnemyPrefab.speed * Time.deltaTime;

            // �N�_�ɒB����������𔽓]
            if (currentPosition.x <= EnemyPrefab.startPosition.x)
            {
                EnemyPrefab.movingRight = true;
                BoundCount++;
            }
        }

        transform.position = currentPosition;

        if(BoundCount >= BoundLimit && BonusEnemy)
        {
            // �I�u�W�F�N�g����莞�Ԍ�ɔj��
            StartCoroutine(FlyAndDestroyCoroutine());
        }
    }
    IEnumerator FlyAndDestroyCoroutine()
    {
        float elapsedTime = 0f;

        // �o�ߎ��Ԃ�destroyTime�ɒB����܂Ń��[�v
        while (elapsedTime < destroyTime)
        {
            // ������Ɉړ�
            transform.Translate(Vector3.up * flySpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null; // ���̃t���[���܂őҋ@
        }

        // ��莞�Ԍo�ߌ�ɃI�u�W�F�N�g���폜
        Destroy(gameObject);
    }
}
