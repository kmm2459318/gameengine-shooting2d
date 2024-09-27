using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class SlideMove : MonoBehaviour
{
    public EnemyController EnemyPrefab;

    //ボーナスエネミーを一定時間後に消すための
    public bool BonusEnemy = false;
    int BoundCount = 0;
    public int BoundLimit = 5;

    public float flySpeed = 1f;   // 上に飛ぶスピード
    public float destroyTime = 5f; // 消えるまでの時間
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
                BoundCount++;
            }
        }
        else
        {
            currentPosition.x -= EnemyPrefab.speed * Time.deltaTime;

            // 起点に達したら方向を反転
            if (currentPosition.x <= EnemyPrefab.startPosition.x)
            {
                EnemyPrefab.movingRight = true;
                BoundCount++;
            }
        }

        transform.position = currentPosition;

        if(BoundCount >= BoundLimit && BonusEnemy)
        {
            // オブジェクトを一定時間後に破壊
            StartCoroutine(FlyAndDestroyCoroutine());
        }
    }
    IEnumerator FlyAndDestroyCoroutine()
    {
        float elapsedTime = 0f;

        // 経過時間がdestroyTimeに達するまでループ
        while (elapsedTime < destroyTime)
        {
            // 上方向に移動
            transform.Translate(Vector3.up * flySpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null; // 次のフレームまで待機
        }

        // 一定時間経過後にオブジェクトを削除
        Destroy(gameObject);
    }
}
