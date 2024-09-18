using TMPro;
using UnityEngine;
using UnityEngine.UI;  // ButtonやImageを使うために必要

public class NowCoin : MonoBehaviour 
{
    public TextMeshProUGUI text;
    public GameObject gameObject;
    public Button buttonObject;  // ボタンを追加
    private Image buttonImage;   // ボタンの画像コンポーネント

    void Start()
    {
        buttonImage = buttonObject.GetComponent<Image>();  // ボタンの画像コンポーネントを取得
    }

    void Update()
    {
        PlayerShotGenerator playerShotGenerator = gameObject.GetComponent<PlayerShotGenerator>();
        int coin = playerShotGenerator.Coin;
        text.SetText(coin.ToString());

        // コインの割合に応じてボタンの色を灰色に変える
        float ratio = Mathf.Clamp01((float)coin / 3);  // 割合を0～1の範囲でクランプ
        Color newColor = Color.Lerp(Color.gray, Color.white, ratio);  // 灰色から白への線形補間
        buttonImage.color = newColor;  // ボタンの色を変更

        if(coin >= 3)
        {
            buttonImage.color =　Color.yellow;
        }
    }
}
