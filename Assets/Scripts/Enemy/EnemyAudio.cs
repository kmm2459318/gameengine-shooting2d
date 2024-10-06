using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    public static EnemyAudio Instance;
    public AudioClip Death; // ���S����ݒ�
    public int maxAudioObjects = 3; // �ő吶����
    private int currentAudioObjects = 0; // ���݂̐�����

    private void Awake()
    {
        // �V���O���g���̐ݒ�
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �V�[�����ς���Ă��I�u�W�F�N�g���c��
        }
        else
        {
            Destroy(gameObject); // ���łɑ��݂���ꍇ�͎�����j��
        }
    }

    // �����Đ����郁�\�b�h
    public void PlayDeathSound(Vector3 position)
    {
        if (currentAudioObjects < maxAudioObjects)
        {
            // �����Đ����邽�߂̈ꎞ�I�ȃI�u�W�F�N�g���쐬
            GameObject audioObject = new GameObject("EnemyDeathSound");
            AudioSource tempAudioSource = audioObject.AddComponent<AudioSource>();
            tempAudioSource.clip = Death;
            tempAudioSource.volume = 0.1f; // ���ʂ�ݒ�
            tempAudioSource.Play();

            // �Đ����I�������I�u�W�F�N�g��j��
            Destroy(audioObject, Death.length);
            currentAudioObjects++; // �J�E���^�𑝂₷

            // �I�[�f�B�I�I�u�W�F�N�g�̐������炷���߂̃R���[�`�����J�n
            StartCoroutine(DecreaseAudioCount());
        }
        else
        {
            Debug.Log("Maximum number of audio objects reached.");
        }
    }

    // �J�E���^�����炷�R���[�`��
    private System.Collections.IEnumerator DecreaseAudioCount()
    {
        yield return new WaitForSeconds(Death.length); // ���̒�����ҋ@
        currentAudioObjects--; // �J�E���^�����炷
    }
}
