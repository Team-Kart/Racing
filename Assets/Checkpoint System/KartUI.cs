using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KartUI : MonoBehaviour
{
    [SerializeField] TMP_Text posText;
    [SerializeField] TMP_Text lapText;

    public void SetLapText(int lap)
    {
        if (lap <= 0)
            lapText.text = "Lap: " + 1;
        else
            lapText.text = "Lap: " + lap;
    }

    public void SetPosText(int pos)
    {
        posText.text = "Position: " + pos;
    }
}
