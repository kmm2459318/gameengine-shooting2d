using System.Collections;
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
    public GameObject GameClear;

    public bool StartGame = false;
    public bool IsGameOver = false;

   

    private void Update()
    {
        PlayerController player = Player.GetComponent<PlayerController>();
        WaveManager wave = Stage.GetComponent<WaveManager>();

        if (player.isSplineFinished)
        {
            WaveManager waveManager = Stage.GetComponent<WaveManager>();
            waveManager.OnPlay = true;
        }

        //GameOver�̏���
        if (player.HP <= 0)
        {
            Stage.SetActive(false);
            GameOver.SetActive(true);
            InGame.SetActive(false);
            IsGameOver = true;
        }
        
        //GameClear�̏���
        if(wave.IsClear == true)
        {
            player.isSplineFinished = false;
            InGame.SetActive(false);
            GameClear.SetActive(true);
            IsGameOver = true;
            // �I�u�W�F�N�g����莞�Ԍ�ɔj��
            player.GameClear();
        }
        
        //Retry����
        if (Input.GetMouseButtonDown(0) && IsGameOver == true)
        {
            IsGameOver = false;
            StartGame = false;
            SceneManager.LoadScene("MainGameScene");
        }
    }
    public void GameStart()
    {
        PlayerController player = Player.GetComponent<PlayerController>();

        //GameStrat�̏���
        if (StartGame == false)
        {
            InGame.SetActive(true);
            StartGame = true;
            Stage.SetActive(true);
            Title.SetActive(false);
        }
       
    }
}