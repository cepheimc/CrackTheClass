using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public Transform target;
    public float speed;
    Vector2[] path;
    int targetIndex;

    private float waitTime;
    public float startWaitTime;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private PathRequestManager m;

    void Start()
    {
        waitTime = startWaitTime;
        target.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
     
        m = GetComponent<PathRequestManager>();
        m.RequestPath(transform.position, target.position, OnPathFound);
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                target.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                waitTime = Random.Range(0, startWaitTime);
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        
        m.RequestPath(transform.position, target.position, OnPathFound);
    }

    public void OnPathFound(Vector2[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
        else
        {
            // Some madness for lector
            waitTime -= 5;
        }
    }
    

    IEnumerator FollowPath()
    {

        if (path.Length == 0)
            yield break;

        Vector2 currentWaypoint = path[0];
        while (true)
        {
            if ((Vector2)transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
          
            yield return null;

        }
        
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
               // Gizmos.DrawCube(path[i], Vector2.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i], path[i]);
                }
            }
        }
    }
}
