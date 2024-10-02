using Unity.VisualScripting;
using UnityEngine;

public class PlayerShotGenerator : MonoBehaviour
{
    // 弾を発射するオブジェクトの位置を取得
    public Transform firePoint;

    //プレイヤーがspline移動中かを取得するため
    public GameObject Player;

    // 弾をオブジェクトとして取得
    public GameObject bulletPrefab1;
    public GameObject bulletPrefab2;
    public GameObject HyperShot;

    private GameObject currentBulletPrefab; // 現在の弾のプレハブ

    public float Reload = 1f; // リロードの速さ
    float PowerUP = 0.5f; // 通常レベルアップ時の射撃間隔をどれだけ速くするか
    float ReloadUP = 3; // ボーナスレベルアップ時の射撃間隔をどれだけ速くするか
    public int Coin = 0; // 現在のコイン数

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
        PlayerController player = Player.GetComponent<PlayerController>();
        ShotController shotController = currentBulletPrefab.GetComponent<ShotController>();
        timeCounter += Time.deltaTime;
        if (timeCounter > reloadTime && player.isSplineFinished)
        {
            timeCounter = 0;
            // 弾を発射
            Instantiate(currentBulletPrefab, firePoint.position, firePoint.rotation);
            shotController.audioSource.PlayOneShot(shotController.ShotSE);
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
            // リロード時間をReloadでスケーリング
            float baseReloadTime = shotController.ReloadTime;

            // Reloadが大きいほどリロード時間が短くなる
            reloadTime = baseReloadTime / (1 + Reload / 10); // 例えば、Powerが10増えるごとにリロード時間が半分になるように調整
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Power")
        {
            Reload += PowerUP;
            // Powerが増えたらリロード時間を再計算
            UpdateReloadTime();
            Destroy(collision.gameObject);
        }
        if(collision.tag == "Bonus")
        {
            Reload += ReloadUP;
            // Powerが増えたらリロード時間を再計算
            UpdateReloadTime();
            Destroy(collision.gameObject);
        }
        if(collision.tag == "coin")
        {
            Coin++;
            Debug.Log(Coin);
            Destroy(collision.gameObject);
        }
    }

    public void HyperShotTrigger()
    {
        if(Coin >= 3)
        {
            Coin -= 3;
            PlayerController playerController = currentBulletPrefab.GetComponent<PlayerController>();
            Instantiate(HyperShot, firePoint.position, firePoint.rotation);
        }
    }

   
}
