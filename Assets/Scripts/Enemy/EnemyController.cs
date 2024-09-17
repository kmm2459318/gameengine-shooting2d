using UnityEngine;
using UnityEngine.Splines;

public class EnemyController : MonoBehaviour
{
    public EnemyData EnemyData;
    int HP;

    public SplineAnimate splineAnimate; // SplineAnimateをアタッチしたオブジェクトを設定

    float speed; // 移動速度
    private Vector3 startPosition;
    private Vector3 endPosition; // 移動範囲の終点
    private bool movingRight = true; // 最初から右方向に動く
    public bool hasReachedEnd = false;

    void Start()
    {
        HP = EnemyData.HP;
        speed = EnemyData.speed;

        // 画面中央の位置を取得
        Vector3 screenCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
        screenCenter.z = 0; // Z軸の位置は不要なので0に設定

        // 移動範囲の設定
        startPosition = new Vector3(screenCenter.x - EnemyData.moveDistance / 2, transform.position.y, transform.position.z);
        endPosition = new Vector3(screenCenter.x + EnemyData.moveDistance / 2, transform.position.y, transform.position.z);

        // 初期位置を設定
        //transform.position = startPosition;
        
        // movingRight は true のまま、右方向に最初から動かす
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;

        if ((splineAnimate == true))
        {
            // Spline上の進行度が1.0に達したかどうかを確認
            if (splineAnimate.normalizedTime >= 1.0f && hasReachedEnd == false)
            {
                Debug.Log("Splineの終了に到達しました！");
                // 終了時の処理をここに追加
                hasReachedEnd = true; // 移動終了
            }
        }
       

        //横移動の処理
        if (movingRight)
        {
            currentPosition.x += speed * Time.deltaTime;

            // 終点に達したら方向を反転
            if (currentPosition.x >= endPosition.x)
            {
                movingRight = false;
            }
        }
        else
        {
            currentPosition.x -= speed * Time.deltaTime;

            // 起点に達したら方向を反転
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
