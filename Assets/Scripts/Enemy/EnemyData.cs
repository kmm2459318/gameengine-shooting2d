using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    public int HP;
    public int attackPower;
    public float speed;
    public float moveDistance;
    public float DropChance;
    public DropItemData[] dropItemData; // �h���b�v�A�C�e���̃v���n�u
}
