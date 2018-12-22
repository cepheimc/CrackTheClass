using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
       /* if (waitTime <= 0)
        {
            transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            waitTime = Random.Range(10, startWaitTime);
        }
        else
        {
            waitTime -= Time.deltaTime;
        }*/

    }
}
