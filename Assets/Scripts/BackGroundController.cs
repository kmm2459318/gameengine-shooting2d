using UnityEngine;
using UnityEngine.UI;

public class BackGroundController : MonoBehaviour
{
    private const float k_maxLength = 1f;
    private const string k_propName = "_MainTex";

    [SerializeField]
    private Vector2 m_offsetSpeed;

    private Material m_materialInstance;

    private void Start()
    {
        if (GetComponent<Image>() is Image i)
        {
            // materialのインスタンスを作成して、他のオブジェクトに影響を与えないようにする
            m_materialInstance = new Material(i.material);
            i.material = m_materialInstance;
        }
    }

    private void Update()
    {
        if (m_materialInstance)
        {
            // xとyの値が0 ～ 1でリピートするようにする
            var x = Mathf.Repeat(Time.time * m_offsetSpeed.x, k_maxLength);
            var y = Mathf.Repeat(Time.time * m_offsetSpeed.y, k_maxLength);
            var offset = new Vector2(x, y);
            m_materialInstance.SetTextureOffset(k_propName, offset);
        }
    }

    private void OnDestroy()
    {
        // ゲームをやめた後にマテリアルのOffsetを戻しておく
        if (m_materialInstance)
        {
            m_materialInstance.SetTextureOffset(k_propName, Vector2.zero);
            Destroy(m_materialInstance);  // メモリリークを防ぐためにインスタンスを削除
        }
    }
}
