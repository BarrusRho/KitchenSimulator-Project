using UnityEngine;

namespace KitchenSimulator.Utility
{
    public class LookAtCamera : MonoBehaviour
    {
        private enum CameraMode
        {
            LookAt,
            LookAtInverted,
            CameraForward,
            CameraForwardInverted
        }

        [SerializeField] private CameraMode _cameraMode;

        private void LateUpdate()
        {
            switch (_cameraMode)
            {
                case CameraMode.LookAt:
                    transform.LookAt(Camera.main.transform);
                    break;
                case CameraMode.LookAtInverted:
                    var directionFromCamera = transform.position - Camera.main.transform.position;
                    transform.LookAt(transform.position + directionFromCamera);
                    break;
                case CameraMode.CameraForward:
                    transform.forward = Camera.main.transform.forward;
                    break;
                case CameraMode.CameraForwardInverted:
                    transform.forward = -Camera.main.transform.forward;
                    break;
            }
        }
    }
}