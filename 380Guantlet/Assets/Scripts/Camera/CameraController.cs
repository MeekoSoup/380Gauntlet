using System;
using UnityEngine;

namespace Camera
{
    public class CameraController : MonoBehaviour
    {
        [Header("Follow Parameters")]
        
        [Tooltip("GameObject to follow.")]
        public Transform target;
        
        [SerializeField, Range(0.01f, 1f), Tooltip("How fast camera follows object. (Smaller number is faster)")]
        private float smoothSpeed = 0.125f;
        
        [SerializeField, Tooltip("Camera offset from transform target.")]
        private Vector3 offset = Vector3.zero;
        
        private Vector3 _velocity = Vector3.zero;

        private void FixedUpdate()
        {
            Vector3 desiredPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, smoothSpeed);
            transform.LookAt(target, Vector3.up);
        }
    }
}