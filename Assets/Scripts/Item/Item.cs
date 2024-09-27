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
}
