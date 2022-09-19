using UnityEngine;

namespace Misc
{
    public class LookAtCamera : MonoBehaviour
    {
        private Camera _mCamera;

        private void Awake()
        {
            _mCamera = Camera.main;
        }

        void LateUpdate()
        {
            transform.LookAt(transform.position + _mCamera.transform.rotation * Vector3.forward,
                _mCamera.transform.rotation * Vector3.up);
        }
    }
}