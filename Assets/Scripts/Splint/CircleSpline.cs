using UnityEngine;
using UnityEngine.Splines;

public class CircleSplinr : MonoBehaviour 
{
    public SplineContainer splineContainer;
    public int numPoints = 20; // 円周上の点の数
    public float radius = 5f; // 円の半径

    void Start()
    {
        CreateCircle();
    }

    void CreateCircle()
    {
        Spline spline = new Spline();
        for (int i = 0; i < numPoints; i++)
        {
            float angle = i * Mathf.PI * 2f / numPoints;
            Vector3 position = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0f);

            // BezierKnotを使用してSplineにポイントを追加
            BezierKnot knot = new BezierKnot(position);
            spline.Add(knot);
        }

        // Splineを閉じる
        spline.Closed = true;

        // SplineをSplineContainerに適用
        splineContainer.Spline = spline;
    }
}