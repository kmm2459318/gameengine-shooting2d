using UnityEngine;
using UnityEngine.Splines;

public class SlideMove : MonoBehaviour
{
    public EnemyController EnemyPrefab;

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
            }
        }
        else
        {
            currentPosition.x -= EnemyPrefab.speed * Time.deltaTime;

            // �N�_�ɒB����������𔽓]
            if (currentPosition.x <= EnemyPrefab.startPosition.x)
            {
                EnemyPrefab.movingRight = true;
            }
        }

        transform.position = currentPosition;

    }
}
