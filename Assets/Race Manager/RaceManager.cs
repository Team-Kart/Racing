using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class RaceManager : NetworkBehaviour
{
    [SerializeField] KartSpawnSystem spawnSystem;

    public static RaceManager Instance { get; private set; }

    public UnityAction<RaceState> OnStateChange;
    public enum RaceState
    {
        WaitingToStart,
        WaitingForPlayers,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    [SerializeField] private NetworkVariable<RaceState> state = new NetworkVariable<RaceState>(RaceState.WaitingToStart);
    private bool isLocalPlayerReady;

    public int LapCount { get; private set; }
    [SerializeField] private int lapcount;
    public NetworkVariable<float> countdownTimer { get; private set; } = new NetworkVariable<float>(3f);

    // Start is called before the first frame update
    void Awake()
    {
        LapCount = lapcount;
        Instance = this;
        if (IsServer)
            state = new NetworkVariable<RaceState>(RaceState.WaitingToStart);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsServer)
        {
            return;
        }

        switch (state.Value)
        {
            case RaceState.WaitingToStart:
                break;
            case RaceState.CountdownToStart:
                countdownTimer.Value -= Time.deltaTime;
                if (countdownTimer.Value < 0f)
                {
                    state.Value = RaceState.GamePlaying;
                }
                break;
            case RaceState.GamePlaying:
                break;
            case RaceState.GameOver:
                break;
        }
    }

    public bool IsGamePlaying()
    {
        return state.Value == RaceState.GamePlaying;
    }

    public void AddPlayer(KartTracker kart)
    {
        kart.SetPosition(spawnSystem.lastPlayerIndex.Value);
    }

    public void HostStartGame()
    {
        if (!IsHost) return;

        state.Value = RaceState.CountdownToStart;
        OnStateChange(state.Value);
    }
}
