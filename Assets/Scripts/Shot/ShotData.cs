using UnityEngine;

[CreateAssetMenu]

public class ShotData : ScriptableObject
{
    public Sprite ShotSprite;
    public string ShotName;
    public int attackPower;
    public float speed;
    public int Compensation = 0; // �K�E�Z�̎����_���[�W�A�ʏ�V���b�g�͎����_�����Ȃ��̂ŏ����l0
}
