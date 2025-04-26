using UnityEngine;
using UnityEngine.Rendering;

public class Portal : MonoBehaviour
{
    [ExecuteInEditMode]
    public Camera portalCamera;
    public Transform pairPortal;

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
        if((camera.cameraType == CameraType.Game || camera.cameraType == CameraType.SceneView) && camera.tag != "PortalCamera")
        {
            portalCamera.projectionMatrix = camera.projectionMatrix;

            var relativePosition = transform.InverseTransformPoint(camera.transform.position);
            relativePosition = Vector3.Scale(relativePosition, new Vector3(-1, 1, -1));
            portalCamera.transform.position = pairPortal.TransformPoint(relativePosition);

            var relativeRotation = transform.InverseTransformDirection(camera.transform.forward);
            relativeRotation = Vector3.Scale(relativeRotation, new Vector3(-1, 1, -1));
            portalCamera.transform.forward = pairPortal.TransformDirection(relativeRotation);
        }
    }
}
