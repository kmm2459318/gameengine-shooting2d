using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour 
{
    int Count = 0;
    int NowWave = 0;  // 現在のWave数
    int MaxWave;
    public GameObject[] Wave;
    public TextMeshProUGUI TextMeshPro;


    void Start()
    {
        NowWaveSet();
        MaxWave = Wave.Length;
        TextMeshPro.SetText(NowWave.ToString() + "/" + MaxWave.ToString());
    }
    void Update()
    {
        //画面内にあるEnemyタグを持った敵キャラをカウントする
        Count = GameObject.FindGameObjectsWithTag("Enemy").Length;
        Debug.Log(Count);
        if(Count == 0)
        {
            NowWaveSet();
        }
    }

    private void NowWaveSet()
    {
        NowWave++;
        if ( NowWave >= Wave.Length + 1 )
        {
            Debug.Log("くりあ");
            SceneManager.LoadScene("GameClear");
        }
        switch (NowWave)
        {
            case 1: Wave[0].SetActive(true); break;
            case 2: Wave[1].SetActive(true); break;
            case 3: Wave[2].SetActive(true); break;
            case 4: Wave[3].SetActive(true); break;
            //case 5: Wave[4].SetActive(true); break;
        }
        TextMeshPro.SetText(NowWave.ToString() + "/" + MaxWave.ToString());
        
    }
}
