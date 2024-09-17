using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.Splines;

public class MoveSpline : MonoBehaviour
{
    public SplineContainer splineContainer; // SplineContainer�ւ̎Q��
    public float TimeSecond = 3f;
    public float speed = 2f; // �ړ����x
    private float progress = 0f; // ���݂̐i�s��


    void Start()
    {
        // ���P�ʂ̎��Ԃ�b�ɕϊ����āA1���ɂ����鑬�x���v�Z
        speed = 1f / TimeSecond;
    }

    void Update()
    {
        // ���ԂɊ�Â��Đi�s�󋵂��X�V
        progress += speed * Time.deltaTime;

        // �i�s�󋵂�1�𒴂����烋�[�v������iSpline�����Ă���ꍇ�j
        if (progress > 1f)
        {
            progress = 0f;
        }

        // Spline��̈ʒu���擾
        Vector3 position = splineContainer.Spline.EvaluatePosition(progress);

        // �I�u�W�F�N�g���ړ�
        transform.position = position;
    }
}