using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Moving : MonoBehaviour
{
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
    private Transform targetTransform;

    void Start()
    {
        var target = new GameObject();
        targetTransform = target.transform;
        targetTransform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        m = GetComponent<PathRequestManager>();
        m.RequestPath(transform.position, targetTransform.position, OnPathFound);
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, targetTransform.position) < 0.2f)
        {
            targetTransform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        }
        // Repeatedly re-calculate path
        if (Random.Range(0, 10) > 5)
            m.RequestPath(transform.position, targetTransform.position, OnPathFound);
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

    
    // Optionally draws path on scene for debugging purposes
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
