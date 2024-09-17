using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour 
{
    int Count = 0;
    int NowWave = 0;  // ���݂�Wave��
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
        //��ʓ��ɂ���Enemy�^�O���������G�L�������J�E���g����
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
            Debug.Log("���肠");
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
