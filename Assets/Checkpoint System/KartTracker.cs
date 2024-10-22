using UnityEngine;
using UnityEngine.Events;

public class KartTracker : MonoBehaviour
{
    public UnityEvent<int> OnLapChange, OnPositionChange;

    public float PositionValue { get; private set; }

    PositionTracker tracker;

    KartData data;

    private void OnEnable()
    {
        data = GetComponent<KartData>();
        tracker = FindObjectOfType<CheckpointSystem>().tracker;
    }
    private void Start()
    {
        tracker.AddKart(this);
        PositionValue = 0;
        data.lap = 0;
        data.nextCheckpointIndex = 0;
        data.prevCheckpointIndex = tracker.checkpoints.Count - 1;

        data.nextCheckpoint = tracker.checkpoints[data.nextCheckpointIndex];
        data.prevCheckpoint = tracker.checkpoints[data.prevCheckpointIndex];
    }

    private void FixedUpdate()
    {
        CalculateCheckpoint();
    }

    public void CalculateCheckpoint()
    {
        Vector3 nextdiff = (transform.position - data.nextCheckpoint.position).normalized;
        Vector3 prevdiff = (transform.position - data.prevCheckpoint.position).normalized;
        if (Vector3.Dot(nextdiff, data.nextCheckpoint.forward) > 0)
        {
            if (Vector3.Distance(transform.position, data.nextCheckpoint.position) < 2f)
                IncrementCheckpoint();
            //Debug.Log(currCheckpointIndex);
        }
        else if (Vector3.Dot(prevdiff, data.prevCheckpoint.forward) < 0)
        {
            DecrementCheckpoint();
            //Debug.Log(currCheckpointIndex);
        }

        PositionValue = ((data.lap + 1) * 100) + ((data.prevCheckpointIndex + 1) * 10) - Vector3.Distance(transform.position, data.nextCheckpoint.position);
        //Debug.Log(PositionValue);
    }

    public void SetPosition(int index)
    {
        Debug.Log(index);
        data.racePosition = index + 1;
        OnPositionChange.Invoke(data.racePosition);
    }

    void IncrementCheckpoint()
    {
        data.nextCheckpointIndex++;
        if (data.nextCheckpointIndex >= tracker.checkpoints.Count)
        {
            data.nextCheckpointIndex = 0;
        }

        data.prevCheckpointIndex++;
        if (data.prevCheckpointIndex >= tracker.checkpoints.Count)
        {
            data.prevCheckpointIndex = 0;
            IncreaseLap();
            Debug.Log("Lap " + data.lap);
        }

        data.nextCheckpoint = tracker.checkpoints[data.nextCheckpointIndex];
        data.prevCheckpoint = tracker.checkpoints[data.prevCheckpointIndex];
    }
    void DecrementCheckpoint()
    {
        data.nextCheckpointIndex--;
        if (data.nextCheckpointIndex < 0)
        {
            data.nextCheckpointIndex = tracker.checkpoints.Count - 1;
        }
        
        data.prevCheckpointIndex--;
        if (data.prevCheckpointIndex < 0)
        {
            data.prevCheckpointIndex = tracker.checkpoints.Count - 1;
            DecreaseLap();
            Debug.Log("Lap " + data.lap);
        }

        data.nextCheckpoint = tracker.checkpoints[data.nextCheckpointIndex];
        data.prevCheckpoint = tracker.checkpoints[data.prevCheckpointIndex];

    }

    void IncreaseLap()
    {
        data.lap++;
        OnLapChange.Invoke(data.lap);
    }
    void DecreaseLap()
    {
        data.lap--;
        if (data.lap < 1)
            data.lap = 0;

        OnLapChange.Invoke(data.lap);
    }
}
