using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Moving : MonoBehaviour
{
    public enum States {
        Standing,  // No target to follow, just standing (start of scene or path finish)
        Calculating,  // New target found, wait until path calculated 
        Moving,  // Moving to target by calculated path
    }
    public float speed;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    protected States state;  // We could do this via state machine but decided that is it not worth here
    protected Transform targetTransform;
    protected int targetIndex = 0;
    protected Vector2[] path;

    private PathRequestManager pathManager;

    void Start()
    {
        targetTransform = (new GameObject()).transform;
        pathManager = GetComponent<PathRequestManager>();
        path = new Vector2[0];
    }

    void Update()
    {
        switch (state)
        {
            case States.Standing:
                targetTransform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                state = States.Calculating;
                pathManager.RequestPath(transform.position, targetTransform.position, OnPathFound);
                break;
            case States.Calculating:
                break;  // just wait till calculating ends
            case States.Moving:
                if (IsPathFinished())
                {
                    StopCoroutine("FollowPath");
                    state = States.Standing;
                }
                break;
        }
    }

    private bool IsPathFinished()
    {
        return (targetTransform.position - transform.position).magnitude <= 0.3 || targetIndex >= path.Length;
    }

    void OnPathFound(Vector2[] newPath, bool pathSuccessful)
    {
        // Debug.Log(string.Format("{0} {1} {2}", pathSuccessful, newPath.Length, state));
        if (newPath.Length == 0 || (state == States.Calculating && !pathSuccessful))
        {
            state = States.Standing;
            return;
        }
        
        state = States.Moving;
        path = newPath;
        
        StartCoroutine("FollowPath");
    }

    IEnumerator FollowPath()
    {
        targetIndex = 0;
        Vector2 currentWaypoint = path[targetIndex];
        while (true)
        {
            if (IsPathFinished())
                yield break;

            // It takes time to reach waypoint, so switch it when object position is equal to waypoint
            if (transform.position == (Vector3) currentWaypoint)
            {
                Debug.Log(1);
                targetIndex++;
                if (targetIndex == path.Length)
                    yield break;
                currentWaypoint = path[targetIndex];
            }
            //Debug.Log(string.Format("From {0} to {1}, should be {2}", transform.position, (Vector3) currentWaypoint, Vector2.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime)));

            transform.position = (Vector3) Vector2.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }
    
    // draws path on scene for debugging purposes
     void OnDrawGizmos()
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
                     Gizmos.DrawLine(path[i - 1], path[i]);
                 }
             }
         }
     }
}
