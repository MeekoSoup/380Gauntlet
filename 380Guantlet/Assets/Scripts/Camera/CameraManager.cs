using System;
using System.Collections.Generic;
using System.Linq;
using Data;

namespace Camera
{
    using UnityEngine;
    
    public class CameraManager : MonoBehaviour
    {
        public EventNetwork eventNetwork;
        public Camera mainCamera;
        public List<GameObject> playerObjects = new List<GameObject>();

        private void Awake()
        {
            playerObjects = GameObject.FindGameObjectsWithTag("Player").ToList();
            
            if (!mainCamera)
                mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            
            if (!mainCamera)
                Debug.Log("CameraManager: No camera with MainCamera tag found!");
        }

        private void Update()
        {
            Debug.Log($"CameraManager: {GetFurthestDistance()}");
        }

        private float GetFurthestDistance()
        {
            float dist = Vector3.Distance(mainCamera.transform.position, playerObjects[0].transform.position);

            
            
            return dist;
        }
    }
}
