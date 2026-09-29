using UnityEngine;

public class CameraObserver : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField][Range(30, 113)] private float _fieldOfView = 90f;

    [SerializeField] private Transform _followPoint;

    private void LateUpdate()
    {
        _camera.transform.position = _followPoint.position;
        _camera.transform.rotation = _followPoint.rotation;
#if UNITY_EDITOR
        _camera.fieldOfView = _fieldOfView;
        //_camera.transform.localPosition = _cameraOffset;
#endif
    }
}