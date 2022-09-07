using UnityEngine;

namespace TestMazeMonsters.Gameplay.Cameras
{
    public class GameplayCamera : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        
        private Transform _transform;
        private Transform _cameraTransform;

        public Transform RootTransform
        {
            get
            {
                if (_transform == null)
                    _transform = transform;
                return _transform;
            }
        }
    
        public Transform CameraTransform
        {
            get
            {
                if (_cameraTransform == null)
                    _cameraTransform = _camera.transform;
                return _cameraTransform;
            }
        }
    }
}
