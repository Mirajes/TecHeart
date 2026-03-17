// using UnityEngine;
// using UnityEngine.Rendering.LWRP;
// using RenderPipeline = UnityEngine.Experimental.Rendering.RenderPipeline;

// [ExecuteInEditMode]
// public class Portal : MonoBehaviour
// {
//     public Camera PortalCamera;
//     public Transform PairPortal;

//     private void OnEnable() {
//         RenderPipeline.beginCameraRendering += UpdateCamera;
//     }

//     private void OnDisable() {
//         RenderPipeline.beginCameraRendering -= UpdateCamera;
//     }

//     private void UpdateCamera(Camera camera) {
//         if ((camera.cameraType == CameraType.Game || CameraType.SceneView) && camera.tag != "Portal Camera")
//         {
//             PortalCamera.projectionMatrix = camera.projectionMatrix;
//             var relativePosition = transform.InverseTransformPoint(camera.transform.position);
//             relativePosition = Vector3.Scale(relativePosition, new Vector3(-1, 1, -1));
//             PortalCamera.transform.position = PairPortal.TransformPoint(relativePosition);

//             var relativeRotation = transform.InverseTransformPoint(camera.transform.forward);
//             relativeRotation = Vector3.Scale(relativeRotation, new Vector3(-1, 1, -1));
//             PortalCamera.transform.forward = PairPortal.TransformDirection(relativeRotation);
//         }
//     }

    
// }
