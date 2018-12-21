using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpot : MonoBehaviour
{
    private float waitTime;
    public float startWaitTime;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    void Start()
    {
        waitTime = startWaitTime;
        transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    void Update()
    {
        

       // if (Vector2.Distance(transform.position, moveSpot.position) < 0.2f)
      //  {
            if (waitTime <= 0)
            {
                transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                waitTime = Random.Range(0, startWaitTime);
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
      //  }
    }
}
