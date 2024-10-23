using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class CheckpointSystem : NetworkBehaviour
{
    [SerializeField] public PositionTrackerSO tracker;
    // Start is called before the first frame update
    void Awake()
    {
        tracker.checkpointParent = this.gameObject;
        tracker.Initialize();
    }

    private void FixedUpdate()
    {
        if (!IsServer) return;
        UpdatePositionPlacementsRpc();
    }

    //[Rpc(SendTo.Server)]
    void UpdatePositionPlacementsRpc()
    {
        //Debug.Log(tracker.karts.Count);
        for (int curr = 0; curr < tracker.karts.Count - 1; curr++)
        {
            int next = curr + 1;
            if (tracker.karts[curr].PositionValue.Value < tracker.karts[next].PositionValue.Value)
            {
                var tempKart = tracker.karts[curr];
                tracker.karts[curr] = tracker.karts[next];
                tracker.karts[curr].SetPosition(curr);

                tracker.karts[next] = tempKart;
                tracker.karts[next].SetPosition(next); 

                Debug.Log("Swapped");
            }
        }
    }

}
