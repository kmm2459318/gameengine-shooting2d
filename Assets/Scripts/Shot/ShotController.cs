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

    //•KŽE‹Z‚ð‘Å‚Á‚½Žž‚Ìˆ—
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                //Œ»ÝHP‚ªÁ”ïHP‚æ‚è‚à‘½‚©‚Á‚½‚ç
                if(playerController.HP > shotData.Compensation)
                {
                    playerController.HP -= shotData.Compensation;
                }
            }
        }
    }
}
