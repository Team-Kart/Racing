using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook cinecam;
    public void ResetCamera(InputAction.CallbackContext ctx)
    {
        cinecam.m_YAxis.Value = .5f;
        cinecam.m_XAxis.Value = 0;
    }
}
