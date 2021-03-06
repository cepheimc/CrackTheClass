﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathRequestManager : MonoBehaviour
{
    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;

    PathFinding pathfinding;

    bool isProcessingPath;

    void Awake()
    {
        var levelGrid = GameObject.Find("LevelGrid");
        pathfinding = levelGrid.GetComponent<PathFinding>();
    }

    public void RequestPath(Vector2 pathStart, Vector2 pathEnd, Action<Vector2[], bool> callback)
    {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        pathRequestQueue.Enqueue(newRequest);
        TryProcessNext();
    }

    void TryProcessNext()
    {
        
        if (!isProcessingPath && pathRequestQueue.Count > 0)
        {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd, FinishedProcessingPath);
        }
    }

    public void FinishedProcessingPath(Vector2[] path, bool success)
    {
        currentPathRequest.callback(path, success);
        isProcessingPath = false;
        TryProcessNext();
    }


    struct PathRequest
    {
        public Vector2 pathStart;
        public Vector2 pathEnd;
        public Action<Vector2[], bool> callback;

        public PathRequest(Vector3 _start, Vector3 _end, Action<Vector2[], bool> _callback)
        {
            pathStart = _start;
            pathEnd = _end;
            callback = _callback;

        }
    }
}
