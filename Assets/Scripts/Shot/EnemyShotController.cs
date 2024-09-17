using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyShotController : MonoBehaviour
{
    public ShotData shotData;
    public float ReloadTime;//射撃間隔の変数
    public int Damage;
    private Transform player; // プレイヤーのTransform
    public int ShotNumber; //ショットの種類を指定する：1、通常弾　2、ホーミング弾
    public float ShotChance; // ショット確率 (0.0 ～ 1.0)

    private Vector2 moveDirection; // プレイヤーへの移動方向
    void Start()
    {
        // プレイヤーのオブジェクトを取得
        Transform player = GameObject.FindWithTag("Player").transform;

        if (player != null)
        {
            // プレイヤーの方向を計算
            Vector2 direction = (player.position - transform.position).normalized;
            moveDirection = direction; // 発射時に移動方向を確定
        }
    }

    void Update()
    {
        switch (ShotNumber)
        {
            // ↓通常弾
            case 0: transform.position += new Vector3(0, shotData.speed, 0) * Time.deltaTime; break;
            // ↓ホーミングショット、確定した方向に弾を移動
            case 1: transform.Translate(moveDirection * shotData.speed * Time.deltaTime); break;

        }
        //画面外に出たらオブジェクトを消す
        if (transform.position.y <= -5)
        {
            Destroy(gameObject);
        }
    }

    // 何かに当たったときの処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 弾が何かに当たったら破壊する
        Destroy(gameObject);
    }
}