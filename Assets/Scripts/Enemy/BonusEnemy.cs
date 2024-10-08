using UnityEngine;

public class BonusEnemy : MonoBehaviour 
{
    public EnemyController EnemyData;

    private void Update()
    {
        if (EnemyData.splineAnimate.normalizedTime >= 1.0f && EnemyData.hasReachedEnd == false)
        {
            Debug.Log("Splineの終了に到達しました！");
            Destroy(gameObject);
        }
    }
}
