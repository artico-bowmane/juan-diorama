using UnityEngine;
using Cinemachine;

public class PortalCameraFollower : MonoBehaviour
{
    private CinemachineBrain brain;
    private ICinemachineCamera lastActiveCam;

    void Start()
    {
        // Try to get CinemachineBrain from the main camera
        brain = Camera.main?.GetComponent<CinemachineBrain>();

        // If no CinemachineBrain is found, log an error and disable the script
        if (brain == null)
        {
            enabled = false;
        }
    }

    void Update()
    {
        // Ensure there's an active virtual camera
        ICinemachineCamera currentCam = brain?.ActiveVirtualCamera;

        // Only update if the active camera has changed
        if (currentCam != null && currentCam != lastActiveCam)
        {
            lastActiveCam = currentCam;
            Debug.Log("Switched to: " + currentCam.Name);
        }
    }

    // Expose the transform of the currently active camera
    public Transform GetActiveCameraTransform()
    {
        // If lastActiveCam is valid, return its transform, else return null
        if (lastActiveCam != null)
            return lastActiveCam.VirtualCameraGameObject.transform;

        return null; // Return null if no active camera
    }
}