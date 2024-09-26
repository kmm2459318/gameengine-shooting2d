using System.Collections;
using UnityEngine;

public class EnemyShotGenerator : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    private float timeCounter = 0f;
    private float reloadTime = 1.0f; // 初期値として1秒
    private int ShotNumber; //ショットの種類を指定する：0、通常弾　1、ホーミング弾　2、拡散弾
    private float ShotChance; // 弾の発射確率
    private int ShotCount; // 拡散弾の弾の数
    private EnemyController enemyController;

    void Start()
    {
        enemyController = GetComponentInParent<EnemyController>();

        if (enemyController != null)
        {
            EnemyShotController shotController = bulletPrefab.GetComponent<EnemyShotController>();
            if (shotController != null)
            {
                reloadTime = shotController.ReloadTime;
                ShotChance = shotController.ShotChance;
                ShotCount = shotController.ShotCount;
                ShotNumber = shotController.ShotNumber;
            }
        }
    }

    void Update()
    {
        if (enemyController != null && enemyController.hasReachedEnd == true)
        {
            timeCounter += Time.deltaTime;
            //体力が3割になったら弾を打つ間隔を1.25倍にする
            if ((enemyController.HP < enemyController.MaxHP / 3) && gameObject.tag == "BossEnemy")
            {
                timeCounter += Time.deltaTime/4;
            }
            // 敵に設定した弾を打つ確率の処理
            if (timeCounter > reloadTime && Random.value < ShotChance)
            {
                timeCounter = 0;
                //球を打つ方法の判断
                //↓拡散弾の発射処理
                if(ShotNumber == 2)
                {
                    for (int i = 0; i < ShotCount; i++)
                    {
                        float bulletAngle = (360f / ShotCount) * i;
                        Quaternion rotation = Quaternion.Euler(0, 0, bulletAngle);
                        Instantiate(bulletPrefab, transform.position, rotation);
                    }
                }
                else
                {
                    StartCoroutine(FireBullets());
                }
                
                
            }
            else if(timeCounter > reloadTime)
            {
                timeCounter = 0;
            }
        }
    }

    
    IEnumerator FireBullets()
    {
        for (int i = 0; i < ShotCount; i++)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            yield return new WaitForSeconds(0.2f); // 0.5秒待機
        }
    }
}
