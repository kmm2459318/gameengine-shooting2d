using UnityEngine;

public class BonusEnemy : MonoBehaviour 
{
    public EnemyController EnemyData;

    private void Update()
    {
        if (EnemyData.splineAnimate.normalizedTime >= 1.0f && EnemyData.hasReachedEnd == false)
        {
            Debug.Log("Spline�̏I���ɓ��B���܂����I");
            Destroy(gameObject);
        }
    }
}
