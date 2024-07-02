using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TeleportOnTrigger : MonoBehaviour
{
    [Tooltip("Target position to teleport the player.")]
    [SerializeField] private Transform _teleportPosition;

    [Tooltip("Cooldown time before the player can be teleported again.")]
    [SerializeField] private float _teleportCooldown = 1f;

    [Tooltip("Optional tag to only work with that tag")]
    [SerializeField] private string _optionalTag;

    private bool _canTeleport = true;

    // Called when the script instance is being loaded
    private void Awake()
    {
        // Check if teleport position is assigned
        if (_teleportPosition == null)
        {
            Debug.LogWarning("Teleport position is not assigned. Destroying game object.");
            Destroy(gameObject); // Destroy this component's game object if teleport position is not set
        }
    }

    // Called when another collider enters the trigger collider attached to this object
    private void OnTriggerEnter(Collider other)
    {
        // Return if teleportation is on cooldown
        if (!_canTeleport) return;

        // Determine the tag to check against
        bool shouldTeleport = string.IsNullOrEmpty(_optionalTag) || other.CompareTag(_optionalTag);

        if (shouldTeleport)
        {
            TeleportPlayerToLocation(other.gameObject);
            StartCoroutine(TeleportCooldownRoutine());
        }

    }

    // Teleport the target object to the teleport position
    private void TeleportPlayerToLocation(GameObject targetObject)
    {
        // Try to get the Rigidbody component of the target object
        if (targetObject.TryGetComponent<Rigidbody>(out Rigidbody targetRB))
        {
            // Use Rigidbody to move the player to avoid physics issues
            targetRB.MovePosition(_teleportPosition.position);
        }
        else
        {
            // Fallback to directly setting the transform position
            targetObject.transform.position = _teleportPosition.position;
        }
    }

    // Coroutine to handle the teleport cooldown
    private IEnumerator TeleportCooldownRoutine()
    {
        // Disable teleportation
        _canTeleport = false;
        // Wait for the cooldown duration
        yield return new WaitForSeconds(_teleportCooldown);
        // Enable teleportation
        _canTeleport = true;
    }
}

#if UNITY_EDITOR
// Custom property drawer to display a dropdown of all tags in Unity editor
[CustomEditor(typeof(TeleportOnTrigger))]
public class TeleportOnTriggerEditor : Editor
{
    SerializedProperty _teleportPositionProp;
    SerializedProperty _teleportCooldownProp;
    SerializedProperty _tagReplacementProp;

    private void OnEnable()
    {
        _teleportPositionProp = serializedObject.FindProperty("_teleportPosition");
        _teleportCooldownProp = serializedObject.FindProperty("_teleportCooldown");
        _tagReplacementProp = serializedObject.FindProperty("_optionalTag");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_teleportPositionProp);
        EditorGUILayout.PropertyField(_teleportCooldownProp);
        _tagReplacementProp.stringValue = EditorGUILayout.TagField("Optional Tag", _tagReplacementProp.stringValue);

        serializedObject.ApplyModifiedProperties();
    }
}
#endif