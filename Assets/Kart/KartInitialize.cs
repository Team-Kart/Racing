using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class KartInitialize : NetworkBehaviour
{
    [SerializeField] PositionTrackerSO tracker;
    public override void OnNetworkSpawn()
    {
        //TODO:: make spawn points on each track
        transform.position = tracker.checkpoints[0].position;
        transform.rotation = tracker.checkpoints[0].rotation;
        transform.position -= transform.forward * 2f;
    }
}
