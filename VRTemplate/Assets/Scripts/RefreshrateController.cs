using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class RefreshrateController : MonoBehaviour
{
    private void Start()
    {
        AttemptChangeDeltaTime();
    }

    private void AttemptChangeDeltaTime()
    {
        // Check if XR device is present
        if (XRSettings.isDeviceActive) ChangeDeltaTime();
        else
        {
            Debug.LogWarning("No XR device is active.");
            Invoke("AttemptChangeDeltaTime", 0.25f);
        }
    }

    private void ChangeDeltaTime()
    {
        // Get the refresh rate of the XR device
        float refreshRate = XRDevice.refreshRate;

        // Check if the refresh rate is valid
        if (refreshRate > 0)
        {
            Time.fixedDeltaTime = 1.0f / refreshRate;
            //Debug.Log("XR Device Refresh Rate: " + refreshRate);
            //Debug.Log("Time.fixedDeltaTime set to: " + Time.fixedDeltaTime);
        }
        else
        {
            Debug.LogWarning("Invalid XR device refresh rate: ");
        }
    }
}
