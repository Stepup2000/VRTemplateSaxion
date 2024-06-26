using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ToggleTeleportationRay : MonoBehaviour
{
    [SerializeField] private GameObject leftTeleportationRay;
    [SerializeField] private GameObject rightTeleportationRay;

    [SerializeField] private InputActionProperty leftActivation;
    [SerializeField] private InputActionProperty rightActivation;


    // Update is called once per frame
    private void Update()
    {
        leftTeleportationRay.SetActive(leftActivation.action.ReadValue<float>() > 0.1f);
        rightTeleportationRay.SetActive(rightActivation.action.ReadValue<float>() > 0.1f);
    }
}
