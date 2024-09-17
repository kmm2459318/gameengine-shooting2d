using UnityEngine;

public class ShotGenerator : MonoBehaviour
{
    // 弾を発射するオブジェクトの位置を取得
    public Transform firePoint;

    // 弾をオブジェクトとして取得
    public GameObject bulletPrefab1;
    public GameObject bulletPrefab2;

    private GameObject currentBulletPrefab; // 現在の弾のプレハブ

    public float Power = 1f;

    private float timeCounter = 1f;
    private float reloadTime = 1.0f; // 初期値として1秒

    void Start()
    {
        // 初期弾をbulletPrefab1に設定
        currentBulletPrefab = bulletPrefab1;
        UpdateReloadTime();
    }

    void Update()
    {
        timeCounter += Time.deltaTime;
        if (Input.GetMouseButton(0) && timeCounter > reloadTime)
        {
            timeCounter = 0;
            // 弾を発射
            Instantiate(currentBulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    public void ChangeButtonDown()
    {
        // 現在の弾のプレハブを切り替え
        if (currentBulletPrefab == bulletPrefab1)
        {
            currentBulletPrefab = bulletPrefab2;
        }
        else
        {
            currentBulletPrefab = bulletPrefab1;
        }
        // 切り替えた弾のリロード時間を更新
        UpdateReloadTime();
    }

    private void UpdateReloadTime()
    {
        // 現在の弾のリロード時間を取得して設定
        ShotController shotController = currentBulletPrefab.GetComponent<ShotController>();
        if (shotController != null)
        {
            // リロード時間をPowerでスケーリング
            float baseReloadTime = shotController.ReloadTime;

            // Powerが大きいほどリロード時間が短くなる
            reloadTime = baseReloadTime / (1 + Power / 10); // 例えば、Powerが10増えるごとにリロード時間が半分になるように調整
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Power")
        {
            Power += 2;

            // Powerが増えたらリロード時間を再計算
            UpdateReloadTime();
        }
    }
}
