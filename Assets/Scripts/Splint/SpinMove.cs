using Unity.VisualScripting;
using UnityEngine;

public class SpinMove: MonoBehaviour
{
    float speed = 1;
    void Update()
    {
        transform.position += new Vector3(0, -speed, 0)*Time.deltaTime;
        if(transform.position.y <= 4)
        {
            speed = 0;
        }
    }
}
