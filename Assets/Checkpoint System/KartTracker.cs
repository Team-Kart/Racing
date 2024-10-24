using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class KartTracker : NetworkBehaviour
{
    public UnityAction<int> OnLapChange, OnPositionChange;

    public NetworkVariable<float> PositionValue { get; private set; } = new NetworkVariable<float>(0);


    KartData data;
    
    public override void OnNetworkSpawn()
    {
        data = GetComponent<KartData>();
        if (IsOwner)
        {
            OnLapChange += FindObjectOfType<KartUI>().SetLapText;
            OnPositionChange += FindObjectOfType<KartUI>().SetPosText;
        }


        if (IsServer)
        {
            data.lap.Value = 0;
            data.nextCheckpointIndex.Value = 0;
            data.prevCheckpointIndex.Value = data.tracker.checkpoints.Count - 1;
            PositionValue.Value = 0;

            data.nextCheckpoint = data.tracker.checkpoints[data.nextCheckpointIndex.Value];
            data.prevCheckpoint = data.tracker.checkpoints[data.prevCheckpointIndex.Value];

            data.tracker.AddKart(this);
        }
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;
        CalculateCheckpointRpc(transform.position);
    }

    [Rpc(SendTo.Server)]
    public void CalculateCheckpointRpc(Vector3 position)
    {
        Vector3 nextdiff = (position - data.nextCheckpoint.position).normalized;
        Vector3 prevdiff = (position - data.prevCheckpoint.position).normalized;
        if (Vector3.Dot(nextdiff, data.nextCheckpoint.forward) > 0 && Vector3.Distance(position, data.nextCheckpoint.position) < data.nextCheckpoint.localScale.x)
        {
            IncrementCheckpointRpc();
            //Debug.Log(data.nextCheckpointIndex.Value);
        }
        else if (Vector3.Dot(prevdiff, data.prevCheckpoint.forward) < 0)
        {
            DecrementCheckpointRpc();
            //Debug.Log(data.nextCheckpointIndex.Value);
        }

        CalculatePositionValueRpc(position, data.nextCheckpoint.position);
        //Debug.Log(PositionValue);
    }

    [Rpc(SendTo.Server)]
    public void CalculatePositionValueRpc(Vector3 player, Vector3 checkpoint)
    {
        PositionValue.Value = ((data.lap.Value + 1) * 100) + ((data.prevCheckpointIndex.Value + 1) * 10) - Vector3.Distance(player, checkpoint);
    }


    public void SetPosition(int index)
    {
        if (!IsServer) return;
        Debug.Log(index);
        data.racePosition.Value = index + 1;

        UpdatePositionClientRpc(data.racePosition.Value);
    }

    [Rpc(SendTo.Server)]
    void IncrementCheckpointRpc()
    {
        data.nextCheckpointIndex.Value++;
        if (data.nextCheckpointIndex.Value >= data.tracker.checkpoints.Count)
        {
            data.nextCheckpointIndex.Value = 0;
        }

        data.prevCheckpointIndex.Value++;
        if (data.prevCheckpointIndex.Value >= data.tracker.checkpoints.Count)
        {
            data.prevCheckpointIndex.Value = 0;
            IncreaseLap();
        }
        
        data.nextCheckpoint = data.tracker.checkpoints[data.nextCheckpointIndex.Value];
        data.prevCheckpoint = data.tracker.checkpoints[data.prevCheckpointIndex.Value];
    }

    [Rpc(SendTo.Server)]
    void DecrementCheckpointRpc()
    {
        data.nextCheckpointIndex.Value--;
        if (data.nextCheckpointIndex.Value < 0)
        {
            data.nextCheckpointIndex.Value = data.tracker.checkpoints.Count - 1;
        }
        
        data.prevCheckpointIndex.Value--;
        if (data.prevCheckpointIndex.Value < 0)
        {
            data.prevCheckpointIndex.Value = data.tracker.checkpoints.Count - 1;
            DecreaseLap();
        }

        data.nextCheckpoint = data.tracker.checkpoints[data.nextCheckpointIndex.Value];
        data.prevCheckpoint = data.tracker.checkpoints[data.prevCheckpointIndex.Value];

    }
    void IncreaseLap()
    {
        if (!IsServer) return;

        data.lap.Value++;
        Debug.Log("Lap: " + data.lap.Value);
        UpdateLapClientRpc(data.lap.Value);
    }
    void DecreaseLap()
    {
        if (!IsServer) return;

        data.lap.Value--;
        if (data.lap.Value < 1)
            data.lap.Value = 0;
        //Debug.Log("Lap: " + data.lap.Value);

        UpdateLapClientRpc(data.lap.Value);
    }

    [Rpc(SendTo.Owner)]
    void UpdateLapClientRpc(int curr)
    {
        //Debug.Log("lap change");
        OnLapChange.Invoke(curr);
    }
    [Rpc(SendTo.Owner)]
    void UpdatePositionClientRpc(int curr)
    {
        //Debug.Log("position change");
        OnPositionChange.Invoke(curr);
    }
}
