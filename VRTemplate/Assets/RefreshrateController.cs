using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class RefreshrateController : MonoBehaviour
{
    void Start()
    {
        // Check if XR device is present
        if (XRSettings.isDeviceActive)
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
                Debug.LogWarning("Invalid XR device refresh rate: " + refreshRate);
            }
        }
        else
        {
            Debug.LogWarning("No XR device is active.");
        }
    }
}
