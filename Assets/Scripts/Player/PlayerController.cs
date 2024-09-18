using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //移動関係
    const float LOAD_WIDTH = 20f;//縦方向の移動可能距離
    const float MOVE_MAX_X = 2f;//横方向の移動可能距離
    const float MOVE_MAX_Y = 4.5f;//横方向の移動可能距離
    Vector2 previousPos;//1f前のマウスの位置
    Vector2 currentPos;//現在のマウスの位置

    //体力関係
    int MaxHP = 100;
    public float HP = 0; // ShotControllerで値を入手するためpublic

    public Image HPbar; // HPBarの画像

    //ダメージ受けたときの点滅
    public Renderer PlayerRenderer; //　プレイヤーの画像データ
    public Color blinkColor = Color.red; // 点滅時の色
    float blinkInterval = 0.1f;   // 点滅の間隔
    int blinkCount = 5; // 点滅の回数
    bool Hit = false; // 点滅中かどうか
    private Color originalColor; // プレイヤーのデフォルトカラー

    

    void Start()
    {
        // 元の色を保存
        originalColor = PlayerRenderer.material.color;
        HP = MaxHP;
    }
    void Update()
    {
        // スワイプによる移動処理
        if (Input.GetMouseButtonDown(0))
        {
            previousPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            // スワイプによる移動距離を取得
            currentPos = Input.mousePosition;
            float diffDistanceX = (currentPos.x - previousPos.x) / Screen.width * LOAD_WIDTH;
            float diffDistanceY = (currentPos.y - previousPos.y) / Screen.width * LOAD_WIDTH;

            // 次のローカルx座標を設定 ※道の外にでないように
            float newX = Mathf.Clamp(transform.localPosition.x + diffDistanceX, -MOVE_MAX_X, MOVE_MAX_X);
            float newY = Mathf.Clamp(transform.localPosition.y + diffDistanceY, -MOVE_MAX_Y, MOVE_MAX_Y);
            transform.localPosition = new Vector3(newX, newY, 0);

            // タップ位置を更新
            previousPos = currentPos;
        }


        if (HP <= 0)
        {
            Debug.Log("aaa");
            Destroy(gameObject);
        }

        //毎回HPを削る
        HP -= 0.02f;
        //HPの更新
        UpdateHPBar();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((Hit == false))
        {
            if (collision.tag == "Enemy")
            {
                Debug.Log("Enemy");
                HP -= 10;
                StartCoroutine(Blink()); // 点滅の呼び出し
            }
            if (collision.tag == "EnemyShot")
            {
                EnemyShotController EnemyShot = collision.GetComponent<EnemyShotController>();
                Destroy(collision.gameObject);
                Debug.Log("Shot");
                HP -= EnemyShot.Damage;
                StartCoroutine(Blink());
            }
        }
        if (collision.tag == "HeelItem")
        {
            HP += 50;
            if(HP >= MaxHP)
            {
                HP = MaxHP;
            }
        }
        UpdateHPBar();
    }

    //HPの更新処理
    void UpdateHPBar()
    {
        float fillAmount = HP / MaxHP; // HPの割合を計算
        HPbar.fillAmount = fillAmount;   // HPバーのfillAmountに反映
    }

    IEnumerator Blink()
    {
        Hit = true;
        for(int i = 1; i<= blinkCount; i++)
        {
            // オブジェクトの色を変更
            PlayerRenderer.material.color = blinkColor;
            yield return new WaitForSeconds(blinkInterval);

            // 元の色に戻す
            PlayerRenderer.material.color = originalColor;
            yield return new WaitForSeconds(blinkInterval);
        }
        Hit = false;
    }
}