using UnityEngine;
using UnityEngine.Splines;

public class SpinMove : MonoBehaviour 
{
    public EnemyController EnemyData;

    public Vector2 CenterPosition = new Vector2(0, 3);

    public float CircleRadius = 1.5f; // 円の半径
    public float moveToCenterDuration = 3f; // 中央に到達するまでの時間

    bool isMovingToCenter = true; // 画面中央に向かって敵が進んでいるか
    private bool isMovingToStartOfCircle = false; // 円運動の開始位置に移動中かどうか
    private bool isCircling = false;         // 円運動をしているかどうか

    private float moveToCenterTime = 0f; // 移動時間のカウント
    private Vector2 startPosition; // 初期位置を保存

    float angle = 0f;

    public GameObject bulletPrefab;     // 弾のPrefab
    public float shootInterval = 5f;    // 弾を撃つ間隔
    public float bulletSpeed = 5f;      // 弾のスピード
    private float shootTimer = 0f;


    private Vector2 startOfCirclePosition; // 円運動の開始位置

    void Start()
    {
        // 現在位置を初期位置として保存
        startPosition = transform.position;

        // 円運動の開始位置を設定 (画面の中心から右に半径分だけ離れた位置に設定)
        startOfCirclePosition = CenterPosition + new Vector2(CircleRadius, 0); // ここで右側の位置を指定
    }

    void Update()
    {
        if (isMovingToCenter)
        {
            // 時間に基づいて中央に移動する
            moveToCenterTime += Time.deltaTime;
            float t = moveToCenterTime / moveToCenterDuration; // 時間の割合
            t = Mathf.Clamp01(t); // 0から1の範囲に制限

            // Lerpを使って徐々に中央に移動
            transform.position = Vector2.Lerp(startPosition, CenterPosition, t);

            if (t >= 1f)
            {
                isMovingToCenter = false;
                isMovingToStartOfCircle = true; // 次は円運動の開始位置に向かう
            }
        }
        else if (isMovingToStartOfCircle)
        {
            // 円運動の開始位置に向かって移動
            transform.position = Vector2.MoveTowards(transform.position, startOfCirclePosition, EnemyData.speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, startOfCirclePosition) < 0.1f)
            {
                isMovingToStartOfCircle = false;
                isCircling = true; // 円運動を開始するフラグを立てる
                EnemyData.hasReachedEnd = true;
            }
        }
        else if (isCircling)
        {
            // 円運動
            angle += EnemyData.speed * Time.deltaTime;
            float x = Mathf.Cos(angle) * CircleRadius;
            float y = Mathf.Sin(angle) * CircleRadius;
            transform.position = CenterPosition + new Vector2(x, y);
        }
    }
}
