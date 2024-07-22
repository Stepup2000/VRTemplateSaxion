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

    [SerializeField] private XRRayInteractor leftRay;
    [SerializeField] private XRRayInteractor rightRay;



    // Update is called once per frame
    private void Update()
    {
        bool isLeftRayHovering = leftRay.TryGetHitInfo(out Vector3 leftPos, out Vector3 leftNormal, out int leftNumber, out bool leftValid);
        bool isRightRayHovering = leftRay.TryGetHitInfo(out Vector3 rightPos, out Vector3 rightNormal, out int rightNumber, out bool righttValid);

        leftTeleportationRay.SetActive(!isLeftRayHovering && leftActivation.action.ReadValue<float>() > 0.1f);
        rightTeleportationRay.SetActive(!isRightRayHovering && rightActivation.action.ReadValue<float>() > 0.1f);
    }
}
