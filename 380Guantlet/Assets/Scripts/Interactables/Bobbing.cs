using System;
using UnityEngine;

namespace Interactables
{
    public class Bobbing : MonoBehaviour
    {
        public float frequency = 1f;
        public float amplitude = 1f;
        private Vector3 _originalPosition;

        private void Awake()
        {
            _originalPosition = transform.position;
        }

        private void Update()
        {
            transform.position = _originalPosition + Vector3.up * Mathf.Sin(Time.time * frequency) * amplitude;
        }
    }
}