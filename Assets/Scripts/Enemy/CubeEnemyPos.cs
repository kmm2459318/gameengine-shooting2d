using UnityEngine;

[DefaultExecutionOrder(-100)] // ”š‚ª¬‚³‚¢‚Ù‚Ç‘‚­Às‚³‚ê‚é
public class CubeEnemyPos : MonoBehaviour 
{
    public int x;
    public int y;
    public float Space_x = 1;
    public float Space_y = 1;

    public GameObject Enemy;
    public Transform GameObject;

    void Start()
    {
        EnemyController controller = Enemy.GetComponent<EnemyController>();
        Vector2 pos = transform.position;
        for(int i = 0; i<=y;  i++)
        {
            for(int j= 0; j<=x; j++)
            {
                controller.hasReachedEnd = true; // ˆÚ“®I—¹
                Vector2 EnemyPos = new Vector2(pos.x + j * Space_x , pos.y + i * Space_y);
                Instantiate(Enemy, EnemyPos, Quaternion.identity, GameObject);
            }
        }
    }

}
