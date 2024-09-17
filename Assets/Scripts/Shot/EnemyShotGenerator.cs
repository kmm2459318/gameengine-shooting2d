using UnityEngine;

public class EnemyShotGenerator : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    private float timeCounter = 0f;
    private float reloadTime = 1.0f; // ‰Šú’l‚Æ‚µ‚Ä1•b
    private float ShotChance; // ’e‚Ì”­ËŠm—¦
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
            }
        }
    }

    void Update()
    {
        if (enemyController != null && enemyController.hasReachedEnd == true)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter > reloadTime && Random.value < ShotChance)
            {
                timeCounter = 0;
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            }
            else if(timeCounter > reloadTime)
            {
                timeCounter = 0;
            }
        }
    }
}
