using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookDirectionTracker : MonoBehaviour
{
    // Maximum distance for the raycast
    [SerializeField, Tooltip("Maximum distance the ray can check for objects.")]
    private float maxRayDistance = 10f;

    // The camera used for raycasting
    [SerializeField, Tooltip("The camera to use for the raycast. Defaults to the first camera found if not assigned.")]
    private Camera playerCamera;

    // Dictionary to store the total look times for each object
    private Dictionary<string, float> lookTimes = new Dictionary<string, float>();

    // The currently looked-at object
    private GameObject currentLookObject = null;

    // The time when the current look began
    private float lookStartTime;

   
    void Start()
    {
        // Attempt to find the first camera in the scene if none is assigned
        Camera[] cameras = FindObjectsOfType<Camera>();
        if (cameras.Length > 0)
        {
            playerCamera = cameras[0];
        }
        else
        {
            Debug.LogError("No camera found in the scene.");
        }
    }

    void Update()
    {
        // Check if the playerCamera is assigned before tracking look time
        if (playerCamera != null)
        {
            TrackLookTime();
            // Visualize the ray in the scene view for debugging
            Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * maxRayDistance, Color.red);
        }
    }

    void TrackLookTime()
    {
        RaycastHit hit;
        // Perform a raycast to detect what the camera is looking at
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, maxRayDistance))
        {
            GameObject hitObject = hit.collider.gameObject;
            Debug.Log(hitObject.name);

            // If the looked-at object has changed, update the look times
            if (hitObject != currentLookObject)
            {
                if (currentLookObject != null)
                {
                    // Calculate the look duration for the previous object
                    float lookDuration = Time.time - lookStartTime;
                    string objectName = currentLookObject.name;

                    // Update the look time in the dictionary
                    if (lookTimes.ContainsKey(objectName))
                    {
                        lookTimes[objectName] += lookDuration;
                    }
                    else
                    {
                        lookTimes[objectName] = lookDuration;
                    }
                }

                // Update the current looked-at object and start time
                currentLookObject = hitObject;
                lookStartTime = Time.time;
            }
        }
        else
        {
            // If no object is being looked at, update the look time for the last object
            if (currentLookObject != null)
            {
                float lookDuration = Time.time - lookStartTime;
                string objectName = currentLookObject.name;

                // Update the look time in the dictionary
                if (lookTimes.ContainsKey(objectName))
                {
                    lookTimes[objectName] += lookDuration;
                }
                else
                {
                    lookTimes[objectName] = lookDuration;
                }

                // Reset current looked-at object
                currentLookObject = null;
            }
        }
    }

    // Method to retrieve the total look time for a given object
    public float GetLookTime(string objectName)
    {
        if (lookTimes.ContainsKey(objectName))
        {
            return lookTimes[objectName];
        }
        return 0f; // Return 0 if the object has no recorded look time
    }

    // Logs the look times for all objects after 30 seconds
    private void LogTime()
    {
        foreach (var entry in lookTimes)
        {
            string objectName = entry.Key;
            float lookDuration = entry.Value;

            // Log the look duration using the DataManager
            DataManager.Instance.AddSubject(objectName, "The player has looked at " + objectName + " for " + lookDuration.ToString("F2") + " seconds.");
        }
    }
}
