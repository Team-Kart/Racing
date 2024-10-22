using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointInitalize : MonoBehaviour
{
    [SerializeField] PositionTracker tracker;
    // Start is called before the first frame update
    void Awake()
    {
        tracker.checkpointParent = this.gameObject;
        tracker.Initialize();
    }
}
