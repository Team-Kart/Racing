using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class KartSpawnSystem : NetworkBehaviour
{
    public NetworkVariable<int> lastPlayerIndex { get; private set; } = new NetworkVariable<int>(0);

    List<Transform> spawns;
    private void Start()
    {
        spawns = new List<Transform>();

        foreach (Transform spawn in GetComponentInChildren<Transform>())
        {
            spawns.Add(spawn);
        }
        spawns.Remove(transform);

        lastPlayerIndex = new NetworkVariable<int>(-1);
        NetworkManager.ConnectionApprovalCallback = ConnectionApprovalCallback;
    }

    void ConnectionApprovalCallback(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        /* you can use this method in your project to customize one of more aspects of the player
         * (I.E: its start position, its character) and to perform additional validation checks. */

        lastPlayerIndex.Value++;
        if (lastPlayerIndex.Value > spawns.Count - 1)
        {
            response.Approved = false;
            return;
        }

        response.Approved = true;
        response.CreatePlayerObject = true;

        response.Position = spawns[lastPlayerIndex.Value].position;
        response.Rotation = spawns[lastPlayerIndex.Value].rotation;
    }

    public void SpawnKartAt(Transform kart, int position)
    {
        kart.transform.position = spawns[position].position;
        kart.transform.rotation = spawns[position].rotation;
    }
}
