using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.Splines;

public class MoveSpline : MonoBehaviour
{
    public SplineContainer splineContainer; // SplineContainerへの参照
    public float TimeSecond = 3f;
    public float speed = 2f; // 移動速度
    private float progress = 0f; // 現在の進行状況


    void Start()
    {
        // 分単位の時間を秒に変換して、1周にかかる速度を計算
        speed = 1f / TimeSecond;
    }

    void Update()
    {
        // 時間に基づいて進行状況を更新
        progress += speed * Time.deltaTime;

        // 進行状況が1を超えたらループさせる（Splineが閉じている場合）
        if (progress > 1f)
        {
            progress = 0f;
        }

        // Spline上の位置を取得
        Vector3 position = splineContainer.Spline.EvaluatePosition(progress);

        // オブジェクトを移動
        transform.position = position;
    }
}