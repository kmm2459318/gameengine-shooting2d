using UnityEngine;
using UnityEngine.Splines;

public class EnemyController : MonoBehaviour
{

    public EnemyData EnemyData;
    public int HP;
    public int MaxHP;

    public SplineAnimate splineAnimate; // SplineAnimateをアタッチしたオブジェクトを設定

    public float speed; // 移動速度
    public Vector3 startPosition;
    public Vector3 endPosition; // 移動範囲の終点
    public bool movingRight = true; // 最初から右方向に動く
    public bool hasReachedEnd = false;


    void Start()
    {
        HP = EnemyData.HP;
        MaxHP = EnemyData.HP;
        transform.rotation = Quaternion.Euler(0, 0, 0);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //必殺技、貫通ショットなので消さない
        if (collision.tag == "HyperShot")
        {
            Damege(collision);
        }
        //通常ショット、貫通しないので消す
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

    //アイテムdropの処理
    private void DropItem()
    {
        if (Random.value < EnemyData.DropChance)
            if (EnemyData.dropItemData != null && EnemyData.dropItemData.Length > 0)
            {
                foreach (var itemData in EnemyData.dropItemData)
                {
                    // dropChanceを使ってアイテムがドロップされるかどうかを判定
                    if (Random.value < itemData.dropChance)
                    {
                        Instantiate(itemData.prefab, transform.position, Quaternion.identity);
                        break; // 1つのアイテムをドロップしたら終了
                    }
                }
            }
    }

    //ダメージの処理
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
