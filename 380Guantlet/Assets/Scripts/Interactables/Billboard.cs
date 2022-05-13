using System;
using System.Collections;
using Control;
using UnityEngine;

namespace Interactables
{
    public class Billboard : MonoBehaviour
    {
        [SerializeField, Tooltip("How often should this update.")]
        private float updateCycleTime = 0.01f;
        
        private GameObject _nearestPlayer;

        private void OnEnable()
        {
            StartCoroutine(FaceNearestPlayer());
        }

        private void OnDisable()
        {
            StopCoroutine(FaceNearestPlayer());
        }

        private IEnumerator FaceNearestPlayer()
        {
            while (true)
            {
                _nearestPlayer = PlayerManager.Instance.GetNearestPlayer(transform);
                if (!_nearestPlayer)
                {
                    yield return new WaitForSeconds(updateCycleTime * 10f);
                    Debug.Log($"Cannot find nearest player!");
                    continue;
                }
                
                var lookRotation = Quaternion.LookRotation(_nearestPlayer.transform.forward, Vector3.up);
                lookRotation.x = 0f;
                lookRotation.z = 0f;
                transform.rotation = lookRotation;
                yield return new WaitForSeconds(updateCycleTime);
            }
        }
    }
}