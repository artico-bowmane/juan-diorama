using UnityEngine;
using UnityEngine.Rendering;

public class Portal : MonoBehaviour
{
    [ExecuteInEditMode]
    public Camera portalCamera;
    public Transform pairPortal;

    // Reference to the PortalCameraFollower script
    [SerializeField] private PortalCameraFollower cameraFollower;

    private void OnEnable()
    {
        RenderPipelineManager.beginCameraRendering += UpdateCamera;
    }

    private void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= UpdateCamera;
    }

    void UpdateCamera(ScriptableRenderContext context, Camera camera)
    {
        if (camera.cameraType == CameraType.Game && camera.tag != "PortalCamera")
        {
            // Get the active camera's transform from the PortalCameraFollower
            Transform activeCamTransform = cameraFollower.GetActiveCameraTransform();
            if (activeCamTransform != null)
            {
                portalCamera.projectionMatrix = camera.projectionMatrix;

                // Calculate relative position for portal camera based on the active camera's position
                var relativePosition = transform.InverseTransformPoint(activeCamTransform.position);
                relativePosition = Vector3.Scale(relativePosition, new Vector3(-1, 1, -1));
                portalCamera.transform.position = pairPortal.TransformPoint(relativePosition);

                // Calculate relative rotation for portal camera based on the active camera's forward direction
                var relativeRotation = transform.InverseTransformDirection(activeCamTransform.forward);
                relativeRotation = Vector3.Scale(relativeRotation, new Vector3(-1, 1, -1));
                portalCamera.transform.forward = pairPortal.TransformDirection(relativeRotation);
            }
        }
    }
}