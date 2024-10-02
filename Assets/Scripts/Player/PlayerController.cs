using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //移動関係
    const float LOAD_WIDTH = 20f;//縦方向の移動可能距離
    const float MOVE_MAX_X = 2f;//横方向の移動可能距離
    const float MOVE_MAX_Y = 4.5f;//横方向の移動可能距離
    Vector2 previousPos;//1f前のマウスの位置
    Vector2 currentPos;//現在のマウスの位置
    public float flySpeed = 0.005f;   // 上に飛ぶスピード
    public float destroyTime = 5f; // 消えるまでの時間

    public SplineAnimate splineAnimate; // SplineAnimateをアタッチしたオブジェクトを設定
    public bool isSplineFinished = false;
    public bool NewStage = false;

    public GameObject SceneController;

    //体力関係
    int MaxHP = 100;
    public float HP = 0; // ShotControllerで値を入手するためpublic
    int HeelPoint = 20; // 回復する量
    int EnemyDamege = 10; // 敵に触れたときのダメージ
    float SlipDamege = 0.025f;
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
        SceneController sceneController = SceneController.GetComponent<SceneController>();
        if (sceneController.StartGame == true)
        {
            splineAnimate.Play();
            // Spline上の進行度が1.0に達したかどうかを確認
            if (!isSplineFinished && splineAnimate.normalizedTime >= 1.0f && NewStage == false)
            {
                Debug.Log("Splineの終了に到達しました！");
                isSplineFinished = true;
                NewStage = true;

                // Spline終了時にタッチ位置をリセット
                previousPos = Input.mousePosition;
            }

            // スワイプによる移動処理はSpline終了後のみ実行
            if (isSplineFinished)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    previousPos = Input.mousePosition;
                }

                if (Input.GetMouseButton(0))
                {
                    // スワイプによる移動距離を取得
                    currentPos = Input.mousePosition;
                    float diffDistanceX = (currentPos.x - previousPos.x) / Screen.width * LOAD_WIDTH * 0.3f;
                    float diffDistanceY = (currentPos.y - previousPos.y) / Screen.width * LOAD_WIDTH * 0.3f;

                    // 次のローカルx座標を設定 ※道の外にでないように
                    float newX = Mathf.Clamp(transform.localPosition.x + diffDistanceX, -MOVE_MAX_X, MOVE_MAX_X);
                    float newY = Mathf.Clamp(transform.localPosition.y + diffDistanceY, -MOVE_MAX_Y, MOVE_MAX_Y);
                    transform.localPosition = new Vector3(newX, newY, 0);

                    // タップ位置を更新
                    previousPos = currentPos;
                }
            }

            if (HP <= 0)
            {
                Debug.Log("aaa");
                gameObject.SetActive(false);
            }

            //HPの更新
            UpdateHPBar();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //自機が点滅していないときのみダメージ処理
        if ((Hit == false))
        {
            //敵本体
            if (collision.tag == "Enemy")
            {
                Debug.Log("Enemy");
                HP -= EnemyDamege;
                StartCoroutine(Blink()); // 点滅の呼び出し
            }
            //敵の弾
            if (collision.tag == "EnemyShot")
            {
                EnemyShotController EnemyShot = collision.GetComponent<EnemyShotController>();
                Destroy(collision.gameObject);
                Debug.Log("Shot");
                HP -= EnemyShot.Damage;
                StartCoroutine(Blink());
            }
        }
        //回復処理
        if (collision.tag == "HeelItem")
        {
            Destroy(collision.gameObject);
            HP += HeelPoint;
            if(HP >= MaxHP)
            {
                HP = MaxHP;
            }
            Destroy(collision.gameObject);
        }
        if (collision.tag == "BonusHeelItem")
        {
            Destroy(collision.gameObject);
            HP = MaxHP;
            Destroy(collision.gameObject);
        }
        UpdateHPBar();
    }

    //HPの更新処理
    void UpdateHPBar()
    {
        float fillAmount = HP / MaxHP; // HPの割合を計算
        HPbar.fillAmount = fillAmount;   // HPバーのfillAmountに反映
    }

    //点滅時の処理
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

    public void GameClear()
    {
        StartCoroutine(FlyAndDestroyCoroutine());
    }
    public IEnumerator FlyAndDestroyCoroutine()
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

        // 一定時間経過後にオブジェクトを非表示
        gameObject.SetActive(false);
    }
}