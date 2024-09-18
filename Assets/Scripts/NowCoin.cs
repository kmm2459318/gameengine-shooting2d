using TMPro;
using UnityEngine;
using UnityEngine.UI;  // Button��Image���g�����߂ɕK�v

public class NowCoin : MonoBehaviour 
{
    public TextMeshProUGUI text;
    public GameObject gameObject;
    public Button buttonObject;  // �{�^����ǉ�
    private Image buttonImage;   // �{�^���̉摜�R���|�[�l���g

    void Start()
    {
        buttonImage = buttonObject.GetComponent<Image>();  // �{�^���̉摜�R���|�[�l���g���擾
    }

    void Update()
    {
        PlayerShotGenerator playerShotGenerator = gameObject.GetComponent<PlayerShotGenerator>();
        int coin = playerShotGenerator.Coin;
        text.SetText(coin.ToString());

        // �R�C���̊����ɉ����ă{�^���̐F���D�F�ɕς���
        float ratio = Mathf.Clamp01((float)coin / 3);  // ������0�`1�͈̔͂ŃN�����v
        Color newColor = Color.Lerp(Color.gray, Color.white, ratio);  // �D�F���甒�ւ̐��`���
        buttonImage.color = newColor;  // �{�^���̐F��ύX

        if(coin >= 3)
        {
            buttonImage.color =�@Color.yellow;
        }
    }
}
