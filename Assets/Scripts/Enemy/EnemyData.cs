using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    public int HP;
    public int attackPower;
    public float speed;
    public float moveDistance;

    public GameObject[] dropItemPrefab; // ドロップアイテムのプレハブ
    public float dropChance = 1f; // ドロップ確率 (0.0 ～ 1.0)
}
