using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using System.Collections;
using System.Collections.Generic;

public class ShotController : MonoBehaviour 
{
    public ShotData shotData;
    public float ReloadTime;//ŽËŒ‚ŠÔŠu‚Ì•Ï”
    public int Damage;

    void Update()
    {
        transform.position += new Vector3(0, shotData.speed, 0) * Time.deltaTime;
        if (transform.position.y > 4.5 || transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }
}
