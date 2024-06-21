using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class AnimateHand : MonoBehaviour
{
    // References to InputActions for grip and trigger inputs
    public InputActionReference gripInputActionReference;
    public InputActionReference triggerInputActionReference;

    private Animator _handAnimator; // Reference to the Animator component
    private float _gripValue; // Current grip input value
    private float _triggerValue; // Current trigger input value

    private void Start()
    {
        // Fetching the Animator component on startup
        _handAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Update the grip and trigger animations every frame
        AnimateGrip();
        AnimateTrigger();
    }

    // Function to animate grip based on input
    private void AnimateGrip()
    {
        // Reading grip input value using InputActionReference
        _gripValue = gripInputActionReference.action.ReadValue<float>();
        // Setting the grip animation parameter in the Animator
        _handAnimator.SetFloat("Grip", _gripValue);
    }

    // Function to animate trigger based on input
    private void AnimateTrigger()
    {
        // Reading trigger input value using InputActionReference
        _triggerValue = triggerInputActionReference.action.ReadValue<float>();
        // Setting the trigger animation parameter in the Animator
        _handAnimator.SetFloat("Trigger", _triggerValue);
    }
}
