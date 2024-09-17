using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    const float LOAD_WIDTH = 20f;//縦方向の移動可能距離
    const float MOVE_MAX_X = 2f;//横方向の移動可能距離
    const float MOVE_MAX_Y = 4.5f;//横方向の移動可能距離
    Vector2 previousPos;//1f前のマウスの位置
    Vector2 currentPos;//現在のマウスの位置
    int MaxHP = 100;
    float HP = 0;

    public Image HPbar;

    void Start()
    {
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
        HP -= 0.03f;
        //HPの更新
        UpdateHPBar();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Debug.Log("Enemy");
            HP -= 10;
        }
        if (collision.tag == "EnemyShot")
        {
            EnemyShotController EnemyShot = collision.GetComponent<EnemyShotController>();
            Destroy(collision.gameObject);
            Debug.Log("Shot");
            HP -= EnemyShot.Damage;
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
}