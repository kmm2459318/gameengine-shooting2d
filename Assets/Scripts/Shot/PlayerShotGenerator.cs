using UnityEngine;

public class ShotGenerator : MonoBehaviour
{
    // �e�𔭎˂���I�u�W�F�N�g�̈ʒu���擾
    public Transform firePoint;

    // �e���I�u�W�F�N�g�Ƃ��Ď擾
    public GameObject bulletPrefab1;
    public GameObject bulletPrefab2;

    private GameObject currentBulletPrefab; // ���݂̒e�̃v���n�u

    public float Power = 1f;

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
        timeCounter += Time.deltaTime;
        if (Input.GetMouseButton(0) && timeCounter > reloadTime)
        {
            timeCounter = 0;
            // �e�𔭎�
            Instantiate(currentBulletPrefab, firePoint.position, firePoint.rotation);
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
            // �����[�h���Ԃ�Power�ŃX�P�[�����O
            float baseReloadTime = shotController.ReloadTime;

            // Power���傫���قǃ����[�h���Ԃ��Z���Ȃ�
            reloadTime = baseReloadTime / (1 + Power / 10); // �Ⴆ�΁APower��10�����邲�ƂɃ����[�h���Ԃ������ɂȂ�悤�ɒ���
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Power")
        {
            Power += 2;

            // Power���������烊���[�h���Ԃ��Čv�Z
            UpdateReloadTime();
        }
    }
}
