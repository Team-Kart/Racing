using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;


[CreateAssetMenu(fileName = "Position Tracker", menuName = "ScriptableObjects/Create Position Tracker")]
public class PositionTrackerSO : ScriptableObject
{
    public List<KartTracker> karts { get; private set; } = new List<KartTracker>();

    public GameObject checkpointParent;
    public List<Transform> checkpoints { get; private set; }

    public void Initialize()
    {
        karts = new List<KartTracker>();

        checkpoints = new List<Transform>();
        foreach (var checkpoint in checkpointParent.gameObject.GetComponentsInChildren<Transform>())
        {
            checkpoints.Add(checkpoint);
        }
        checkpoints.RemoveAt(0);
        Debug.Log("added " + checkpoints.Count);
    }

    public void AddKart(KartTracker kart)
    {
        Debug.Log("Added");
        karts.Add(kart);
    }
}
