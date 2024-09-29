using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject InGame;
    public GameObject Player;
    public GameObject Stage;
    public GameObject Title;
    public GameObject GameOver;

    public bool StartGame = false;
    public bool IsGameOver = false;

    private void Start()
    {
        
    }
    private void Update()
    {
        PlayerController player = Player.GetComponent<PlayerController>();
        if (Input.GetMouseButtonDown(0) && StartGame == false)
        {
            InGame.SetActive(true);
            StartGame = true;
            Stage.SetActive(true);
            Title.SetActive(false);
        }
        if (player.isSplineFinished)
        {
            WaveManager waveManager = Stage.GetComponent<WaveManager>();
            waveManager.OnPlay = true;
        }
        if(player.HP <= 0)
        {
            Stage.SetActive(false);
            GameOver.SetActive(true);
            IsGameOver = true;
        }
        if (Input.GetMouseButtonDown(0) && IsGameOver == true)
        {
            IsGameOver = false;
            StartGame = false;
            SceneManager.LoadScene("MainGameScene");
        }
    }
}