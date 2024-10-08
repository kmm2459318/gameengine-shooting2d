using UnityEngine;

[DefaultExecutionOrder(-100)] // 数字が小さいほど早く実行される
public class CubeEnemyPos : MonoBehaviour 
{
    public int x;
    public int y;
    public float Space_x = 1;
    public float Space_y = 1;

    public GameObject Enemy;
    public Transform GameObject;
    public EnemyController EnemyData;

    void Start()
    {
        SlideMove controller = Enemy.GetComponent<SlideMove>();
        Vector2 pos = transform.position;
        for(int i = 0; i<=y;  i++)
        {
            for(int j= 0; j<=x; j++)
            {
                EnemyData.hasReachedEnd = true; // 移動終了
                Vector2 EnemyPos = new Vector2(pos.x + j * Space_x , pos.y + i * Space_y);
                Instantiate(Enemy, EnemyPos, Quaternion.identity, GameObject);
            }
        }
    }

}
