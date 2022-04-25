using System.Collections.Generic;
using System.Linq;
using Data;

namespace Camera
{
    using UnityEngine;
    
    /**
     * CameraManager is designed to search for targets by their GameObject tags, and attempt to keep them all in view
     * while they move around. It will change its rotation and field of view to try to keep them all in view, but it
     * will not change its original position.
     *
     * TODO: If the position of the camera itself needs to change, then parts of this script need to be rewritten.
     *
     * Properties for this class are described below via Headers/Tooltips.
     *
     * CameraManager invokes EventNetwork.OnPlayerLeavesCameraZone with no PlayerData right now, because it currently
     * doesn't have a way to extract that data from the objects it collects.
     *
     * TODO: Rewrite when PlayerData can be extracted from the GameObjects.
     */
    
    [RequireComponent(typeof(Camera))]
    public class CameraManager : MonoBehaviour
    {
        public EventNetwork eventNetwork;
        [Tooltip("The tag to search for when looking for objects to focus on.")]
        public string targetTag = "Player";
        [Header("LookAt Properties")]
        public Vector3 offset;
        [Tooltip("Time it takes (in seconds) until camera ends its movement.")]
        public float smoothTime = 0.4f;
        [Header("Zoom Properties")]
        public float zoomMin = 40f;
        public float zoomMax = 10f;
        [Tooltip("Adjusts the easing for the zoom. A larger number produces a slower easing.")]
        public float zoomLimiter = 50f;
        [Header("Camera Zone Properties")]
        [Tooltip("How much \"wiggle room\" to give players before they are considered outside of the camera zone.")]
        public float zonePadding = 5f;

        private Camera _cam;
        private Vector3 _velocity;
        private List<GameObject> _playerObjects = new List<GameObject>();
        private Bounds _bounds;
        private bool _playersChanged;
        private float _dist;
        private float _newZoom;

        private void Awake()
        {
            _cam = GetComponent<Camera>();
            _playersChanged = true;
        }

        private void OnEnable()
        {
            // whenever players could change, make sure to recollect available player objects
            eventNetwork.OnPlayerJoined += PlayersChanged;
            eventNetwork.OnPlayerKilled += PlayersChanged;
            eventNetwork.OnPlayerDisconnect += PlayersChanged;
            eventNetwork.OnPlayerReconnect += PlayersChanged;
            eventNetwork.OnPlayerRevived += PlayersChanged;
            eventNetwork.OnPlayerExitStage += PlayersChanged;
        }

        private void OnDisable()
        {
            eventNetwork.OnPlayerJoined -= PlayersChanged;
            eventNetwork.OnPlayerKilled -= PlayersChanged;
            eventNetwork.OnPlayerDisconnect -= PlayersChanged;
            eventNetwork.OnPlayerReconnect -= PlayersChanged;
            eventNetwork.OnPlayerRevived -= PlayersChanged;
            eventNetwork.OnPlayerExitStage -= PlayersChanged;
        }

        private void LateUpdate()
        {
            UpdateBounds();
            
            if (_playerObjects.Count == 0) return;

            LookAtTargets();
            ZoomToTargets();
        }

        private void PlayersChanged(PlayerData playerData = null)
        {
            // TODO: when player data can be extracted from the player objects, amend this!
            _playersChanged = true;
        }

        private void UpdateBounds()
        {
            if (_playersChanged)
            {
                _playerObjects = GameObject.FindGameObjectsWithTag(targetTag).ToList();
                _playersChanged = false;
            }
            
            _bounds = new Bounds();

            foreach (var playerObject in _playerObjects)
            {
                _bounds.Encapsulate(playerObject.transform.position);
            }
        }

        private void LookAtTargets()
        {
            Vector3 centerPoint = GetCenterPoint();
            Vector3 newPosition = centerPoint + offset;
            
            _cam.transform.LookAt(Vector3.SmoothDamp(transform.position, newPosition, ref _velocity, smoothTime));
        }

        private Vector3 GetCenterPoint()
        {
            if (_playerObjects.Count == 0) return Vector3.zero;
            if (_playerObjects.Count == 1) return _playerObjects[0].transform.position;

            return _bounds.center;
        }

        private void ZoomToTargets()
        {
            _dist = Mathf.Max(_bounds.max.x, _bounds.max.z);
            _newZoom = Mathf.Lerp(zoomMax, zoomMin, Mathf.Clamp01(_dist / zoomLimiter));

            if (_dist > _cam.fieldOfView - zonePadding)
            {
                // Someone left the camera zone, now all players must suffer!
                eventNetwork.OnPlayerLeavesCameraZone?.Invoke();
            }
            
            _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, _newZoom, Time.deltaTime);
        }
    }
}
