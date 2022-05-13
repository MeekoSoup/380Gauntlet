using System;
using System.Collections;
using Character;
using UnityEngine;

namespace Interactables
{
    public class Door : MonoBehaviour
    {
        [SerializeField]
        private Vector3 moveDistance = Vector3.down * 3;
        
        [SerializeField, Range(0.001f, 0.1f)]
        private float moveSpeed = 0.01f;
        
        private bool _isOpen;
        private Vector3 _originalPosition;

        private void Awake()
        {
            _originalPosition = transform.position;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_isOpen) return;
            
            var po = collision.gameObject.GetComponent<PlayerOverseer>();
            if (!po) return;
            if (po.playerData.keys <= 0) return;
            
            po.playerData.keys--;
            StartCoroutine(OpenDoor());
        }

        private IEnumerator OpenDoor()
        {
            _isOpen = true;
            float time = 0f;
            while (true)
            {
                transform.position = Vector3.Lerp(_originalPosition, _originalPosition + moveDistance, time);
                time = Mathf.Clamp01(time + moveSpeed);
                yield return new WaitForSeconds(0.001f);
                if (time >= 1f)
                    break;
            }

            yield return null;
        }
    }
}