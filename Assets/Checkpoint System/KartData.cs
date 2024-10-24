using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class KartData : NetworkBehaviour
{
    public NetworkVariable<int> nextCheckpointIndex = new NetworkVariable<int>(0);
    public NetworkVariable<int> prevCheckpointIndex = new NetworkVariable<int>(0);
    public Transform nextCheckpoint;
    public Transform prevCheckpoint;
    public NetworkVariable<int> lap = new NetworkVariable<int>(0);

    public NetworkVariable<int> racePosition = new NetworkVariable<int>(0);

    public PositionTrackerSO tracker;

    [Rpc(SendTo.Server)]
    public void SetCheckpointsRpc()
    {
        nextCheckpoint = tracker.checkpoints[nextCheckpointIndex.Value];
        prevCheckpoint = tracker.checkpoints[prevCheckpointIndex.Value];
    }
}
