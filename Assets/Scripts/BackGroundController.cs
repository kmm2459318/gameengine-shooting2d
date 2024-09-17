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
            // material�̃C���X�^���X���쐬���āA���̃I�u�W�F�N�g�ɉe����^���Ȃ��悤�ɂ���
            m_materialInstance = new Material(i.material);
            i.material = m_materialInstance;
        }
    }

    private void Update()
    {
        if (m_materialInstance)
        {
            // x��y�̒l��0 �` 1�Ń��s�[�g����悤�ɂ���
            var x = Mathf.Repeat(Time.time * m_offsetSpeed.x, k_maxLength);
            var y = Mathf.Repeat(Time.time * m_offsetSpeed.y, k_maxLength);
            var offset = new Vector2(x, y);
            m_materialInstance.SetTextureOffset(k_propName, offset);
        }
    }

    private void OnDestroy()
    {
        // �Q�[������߂���Ƀ}�e���A����Offset��߂��Ă���
        if (m_materialInstance)
        {
            m_materialInstance.SetTextureOffset(k_propName, Vector2.zero);
            Destroy(m_materialInstance);  // ���������[�N��h�����߂ɃC���X�^���X���폜
        }
    }
}
