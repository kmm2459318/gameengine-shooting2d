using UnityEngine;

[CreateAssetMenu]
public class DropItemData : ScriptableObject
{
    public GameObject prefab; // 実際のアイテムのプレハブ
    public float dropChance; // ドロップ率 (0から1の間)
}