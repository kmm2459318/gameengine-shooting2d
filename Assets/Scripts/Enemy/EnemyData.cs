using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    public int HP;
    public int attackPower;
    public float speed;
    public float moveDistance;

    public GameObject[] dropItemPrefab; // �h���b�v�A�C�e���̃v���n�u
    public float dropChance = 1f; // �h���b�v�m�� (0.0 �` 1.0)
}
