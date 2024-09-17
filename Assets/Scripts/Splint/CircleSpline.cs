using UnityEngine;
using UnityEngine.Splines;

public class CircleSplinr : MonoBehaviour 
{
    public SplineContainer splineContainer;
    public int numPoints = 20; // �~����̓_�̐�
    public float radius = 5f; // �~�̔��a

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

            // BezierKnot���g�p����Spline�Ƀ|�C���g��ǉ�
            BezierKnot knot = new BezierKnot(position);
            spline.Add(knot);
        }

        // Spline�����
        spline.Closed = true;

        // Spline��SplineContainer�ɓK�p
        splineContainer.Spline = spline;
    }
}