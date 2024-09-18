using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using System.Collections;
using System.Collections.Generic;

public class ShotController : MonoBehaviour 
{
    public ShotData shotData;
    public float ReloadTime;//射撃間隔の変数
    public int Damage;
    

    void Update()
    {
        transform.position += new Vector3(0, shotData.speed, 0) * Time.deltaTime;
        if (transform.position.y > 4.5 || transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }

    //必殺技を打った時の処理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                //現在HPが消費HPよりも多かったら
                if(playerController.HP > shotData.Compensation)
                {
                    playerController.HP -= shotData.Compensation;
                }
            }
        }
    }
}
