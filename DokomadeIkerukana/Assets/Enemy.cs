using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int direction;
    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        direction = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if(direction == 0)
        {
           transform.position -= new Vector3(Time.deltaTime * 1f, 0, 0);
        }
        if(direction == 1)
        {
           transform.position += new Vector3(Time.deltaTime * 1f, 0, 0);
        }

        time += Time.deltaTime;
        if(time >= 20)
        {
            Destroy(this);
        }
    }
}
