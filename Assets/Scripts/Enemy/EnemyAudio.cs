using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    public static EnemyAudio Instance;
    public AudioClip Death; // 死亡音を設定
    public int maxAudioObjects = 3; // 最大生成数
    private int currentAudioObjects = 0; // 現在の生成数

    private void Awake()
    {
        // シングルトンの設定
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンが変わってもオブジェクトを残す
        }
        else
        {
            Destroy(gameObject); // すでに存在する場合は自分を破棄
        }
    }

    // 音を再生するメソッド
    public void PlayDeathSound(Vector3 position)
    {
        if (currentAudioObjects < maxAudioObjects)
        {
            // 音を再生するための一時的なオブジェクトを作成
            GameObject audioObject = new GameObject("EnemyDeathSound");
            AudioSource tempAudioSource = audioObject.AddComponent<AudioSource>();
            tempAudioSource.clip = Death;
            tempAudioSource.volume = 0.1f; // 音量を設定
            tempAudioSource.Play();

            // 再生が終わったらオブジェクトを破棄
            Destroy(audioObject, Death.length);
            currentAudioObjects++; // カウンタを増やす

            // オーディオオブジェクトの数を減らすためのコルーチンを開始
            StartCoroutine(DecreaseAudioCount());
        }
        else
        {
            Debug.Log("Maximum number of audio objects reached.");
        }
    }

    // カウンタを減らすコルーチン
    private System.Collections.IEnumerator DecreaseAudioCount()
    {
        yield return new WaitForSeconds(Death.length); // 音の長さを待機
        currentAudioObjects--; // カウンタを減らす
    }
}
