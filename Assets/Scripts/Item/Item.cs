using UnityEngine;

public class Item : MonoBehaviour 
{
    void Update()
    {
        transform.position += new Vector3(0, -3, 0) * Time.deltaTime;
        if(transform.position.y <= -5 )
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
