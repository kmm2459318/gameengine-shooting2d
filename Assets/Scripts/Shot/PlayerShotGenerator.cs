using Unity.VisualScripting;
using UnityEngine;

public class PlayerShotGenerator : MonoBehaviour
{
    // �e�𔭎˂���I�u�W�F�N�g�̈ʒu���擾
    public Transform firePoint;

    //�v���C���[��spline�ړ��������擾���邽��
    public GameObject Player;

    // �e���I�u�W�F�N�g�Ƃ��Ď擾
    public GameObject bulletPrefab1;
    public GameObject bulletPrefab2;
    public GameObject HyperShot;

    private GameObject currentBulletPrefab; // ���݂̒e�̃v���n�u

    public float Reload = 1f; // �����[�h�̑���
    float PowerUP = 0.5f; // �ʏ탌�x���A�b�v���̎ˌ��Ԋu���ǂꂾ���������邩
    float ReloadUP = 3; // �{�[�i�X���x���A�b�v���̎ˌ��Ԋu���ǂꂾ���������邩
    public int Coin = 0; // ���݂̃R�C����

    private float timeCounter = 1f;
    private float reloadTime = 1.0f; // �����l�Ƃ���1�b

    void Start()
    {
        // �����e��bulletPrefab1�ɐݒ�
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
            // �e�𔭎�
            Instantiate(currentBulletPrefab, firePoint.position, firePoint.rotation);
            shotController.audioSource.PlayOneShot(shotController.ShotSE);
        }
    }

    public void ChangeButtonDown()
    {
        // ���݂̒e�̃v���n�u��؂�ւ�
        if (currentBulletPrefab == bulletPrefab1)
        {
            currentBulletPrefab = bulletPrefab2;
        }
        else
        {
            currentBulletPrefab = bulletPrefab1;
        }
        // �؂�ւ����e�̃����[�h���Ԃ��X�V
        UpdateReloadTime();
    }

    private void UpdateReloadTime()
    {
        // ���݂̒e�̃����[�h���Ԃ��擾���Đݒ�
        ShotController shotController = currentBulletPrefab.GetComponent<ShotController>();
        if (shotController != null)
        {
            // �����[�h���Ԃ�Reload�ŃX�P�[�����O
            float baseReloadTime = shotController.ReloadTime;

            // Reload���傫���قǃ����[�h���Ԃ��Z���Ȃ�
            reloadTime = baseReloadTime / (1 + Reload / 10); // �Ⴆ�΁APower��10�����邲�ƂɃ����[�h���Ԃ������ɂȂ�悤�ɒ���
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Power")
        {
            Reload += PowerUP;
            // Power���������烊���[�h���Ԃ��Čv�Z
            UpdateReloadTime();
            Destroy(collision.gameObject);
        }
        if(collision.tag == "Bonus")
        {
            Reload += ReloadUP;
            // Power���������烊���[�h���Ԃ��Čv�Z
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
