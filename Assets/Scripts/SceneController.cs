using TMPro;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public GameObject InGame;
    public GameObject Player;
    public GameObject Stage;
    public GameObject Title;

    public bool StartGame = false;

    private void Update()
    {
        PlayerController player = Player.GetComponent<PlayerController>();
        if (Input.GetMouseButtonDown(0))
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
    }
}
