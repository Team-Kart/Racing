using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;


[CreateAssetMenu(fileName = "Position Tracker", menuName = "ScriptableObjects/Create Position Tracker")]
public class PositionTracker : ScriptableObject
{
    public List<Transform> karts { get; private set; } = new List<Transform>();

    public GameObject checkpointParent;
    public List<Transform> checkpoints { get; private set; }

    public void Initialize()
    {
        checkpoints = new List<Transform>();
        foreach (var checkpoint in checkpointParent.gameObject.GetComponentsInChildren<Transform>())
        {
            checkpoints.Add(checkpoint);
        }
        checkpoints.RemoveAt(0);
        Debug.Log("added " + checkpoints.Count);
    }
}
