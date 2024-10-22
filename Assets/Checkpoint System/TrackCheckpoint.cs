using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class TrackCheckpoint : MonoBehaviour
{
    [SerializeField] int currCheckpointIndex;
    [SerializeField] int prevCheckpointIndex;
    [SerializeField] Transform currCheckpoint;
    [SerializeField] Transform prevCheckpoint;
    [SerializeField] PositionTracker tracker;

    [SerializeField] int lap;
    private void Start()
    {
        lap = 0;
        currCheckpointIndex = 0;
        prevCheckpointIndex = tracker.checkpoints.Count - 1;

        currCheckpoint = tracker.checkpoints[currCheckpointIndex];
        prevCheckpoint = tracker.checkpoints[prevCheckpointIndex];
    }

    private void Update()
    {
        CalculateCheckpoint();
    }

    void CalculateCheckpoint()
    {
        Vector3 currdiff = (transform.position - currCheckpoint.position).normalized;
        Vector3 prevdiff = (transform.position - prevCheckpoint.position).normalized;
        if (Vector3.Dot(currdiff, currCheckpoint.forward) > 0)
        {
            if (Vector3.Distance(transform.position, currCheckpoint.position) < 2f)
                IncrementCheckpoint();
            //Debug.Log(currCheckpointIndex);
        }
        else if (Vector3.Dot(prevdiff, prevCheckpoint.forward) < 0)
        {
            DecrementCheckpoint();
            //Debug.Log(currCheckpointIndex);
        }
    }

    void IncrementCheckpoint()
    {
        currCheckpointIndex++;
        if (currCheckpointIndex >= tracker.checkpoints.Count)
        {
            currCheckpointIndex = 0;
        }

        prevCheckpointIndex++;
        if (prevCheckpointIndex >= tracker.checkpoints.Count)
        {
            prevCheckpointIndex = 0;
            IncreaseLap();
            Debug.Log("Lap " + lap);
        }

        currCheckpoint = tracker.checkpoints[currCheckpointIndex];
        prevCheckpoint= tracker.checkpoints[prevCheckpointIndex];
    }
    void DecrementCheckpoint()
    {
        currCheckpointIndex--;
        if (currCheckpointIndex < 0)
        {
            currCheckpointIndex = tracker.checkpoints.Count - 1;
        }
        
        prevCheckpointIndex--;
        if (prevCheckpointIndex < 0)
        {
            prevCheckpointIndex = tracker.checkpoints.Count - 1;
            DecreaseLap();
            Debug.Log("Lap " + lap);
        }

        currCheckpoint = tracker.checkpoints[currCheckpointIndex];
        prevCheckpoint = tracker.checkpoints[prevCheckpointIndex];

    }

    void IncreaseLap()
    {
        lap++;
    }
    void DecreaseLap()
    {
        lap--;
        if (lap < 1)
            lap = 0;
    }
}
