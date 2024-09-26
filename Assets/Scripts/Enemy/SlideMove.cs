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
;            // Spline上の進行度が1.0に達したかどうかを確認
            if (EnemyPrefab.splineAnimate.normalizedTime >= 1.0f && EnemyPrefab.hasReachedEnd == false)
            {
                Debug.Log("Splineの終了に到達しました！");
                // 終了時の処理をここに追加
                EnemyPrefab.hasReachedEnd = true; // 移動終了
            }
        }


        //横移動の処理
        if (EnemyPrefab.movingRight)
        {
            currentPosition.x += EnemyPrefab.speed * Time.deltaTime;

            // 終点に達したら方向を反転
            if (currentPosition.x >= EnemyPrefab.endPosition.x)
            {
                EnemyPrefab.movingRight = false;
            }
        }
        else
        {
            currentPosition.x -= EnemyPrefab.speed * Time.deltaTime;

            // 起点に達したら方向を反転
            if (currentPosition.x <= EnemyPrefab.startPosition.x)
            {
                EnemyPrefab.movingRight = true;
            }
        }

        transform.position = currentPosition;

    }
}
