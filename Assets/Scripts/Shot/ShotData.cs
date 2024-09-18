using UnityEngine;

[CreateAssetMenu]

public class ShotData : ScriptableObject
{
    public Sprite ShotSprite;
    public string ShotName;
    public int attackPower;
    public float speed;
    public int Compensation = 0; // 必殺技の自傷ダメージ、通常ショットは自傷ダメがないので初期値0
}
