using Cinemachine;
using UnityEngine;

public class CustomCamera : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private CinemachineImpulseSource _cinemachineImpulseSource;

    #region getters

    public CinemachineVirtualCamera VirtualCamera => _virtualCamera;

    #endregion

    private CinemachineTransposer _cinemachineTransposer;

    private void Awake() {
        _cinemachineTransposer = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
    }

    public void SetFollowTarget(Transform target, Transform lookAtTarget = default) {
        
        _virtualCamera.Follow = target;
     
        if (lookAtTarget != default) {
            _virtualCamera.LookAt = target;
        } else {
            _virtualCamera.LookAt = null;
        }
    }

    public void ShakeCamera() {
        _cinemachineImpulseSource.GenerateImpulse();
    }
}
