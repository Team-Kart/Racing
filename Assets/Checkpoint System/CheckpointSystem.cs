using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    [SerializeField] public PositionTracker tracker;
    // Start is called before the first frame update
    void Awake()
    {
        tracker.checkpointParent = this.gameObject;
        tracker.Initialize();
    }

    private void FixedUpdate()
    {
        UpdatePositionPlacements();
    }

    void UpdatePositionPlacements()
    {
        for (int curr = 0; curr < tracker.karts.Count - 1; curr++)
        {
            int next = curr + 1;
            if (tracker.karts[curr].PositionValue < tracker.karts[next].PositionValue)
            {
                var tempKart = tracker.karts[curr];
                tracker.karts[curr] = tracker.karts[next];
                tracker.karts[curr].SetPosition(curr);

                tracker.karts[next] = tempKart;
                tracker.karts[next].SetPosition(next);
            }
        }
    }
}
