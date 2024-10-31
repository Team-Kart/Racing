using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class StartRaceUI : NetworkBehaviour
{
    [SerializeField] TMP_Text waitToStartText;
    [SerializeField] GameObject UI;

    public override void OnNetworkSpawn()
    {
        RaceManager.Instance.OnStateChange += BeginCountdownRpc;
        if (IsHost)
        {
            waitToStartText.text = "Press Start to begin the race!";
        }
        else
        {
            waitToStartText.text = "Waiting for host...";
        }
    }

    [Rpc (SendTo.ClientsAndHost)]
    public void BeginCountdownRpc(RaceManager.RaceState state)
    {
        if (state == RaceManager.RaceState.CountdownToStart)
            StartCoroutine(Countdown(RaceManager.Instance.countdownTimer.Value));
    }

    public IEnumerator Countdown(float countdown)
    {
        float timer = countdown;

        while (timer > 0)
        {
            waitToStartText.text = "Beginning in " + Mathf.RoundToInt(timer) + "...";
            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        UI.SetActive(false);
    }
}
