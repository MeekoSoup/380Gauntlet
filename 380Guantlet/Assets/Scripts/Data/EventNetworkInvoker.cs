using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Data
{
    public class EventNetworkInvoker : MonoBehaviour
    {
        public EventNetwork eventNetwork;
        [SerializeField]
        private PlayerInputManager inputManager;

        public List<PlayerInput> playerObjects = new List<PlayerInput>();

        private void Awake()
        {
            if (inputManager && transform.parent)
                inputManager = transform.parent.GetComponent<PlayerInputManager>();
        }

        public void PlayerJoined()
        {
            foreach (var playerInput in PlayerInput.all)
            {
                if (playerObjects.Contains(playerInput))
                    Debug.Log($"{playerInput.devices[0].deviceId.ToString()} already active!");
                else
                {
                    playerObjects.Add(playerInput);
                    Debug.Log($"{playerInput.devices[0].deviceId.ToString()} added!");
                    eventNetwork.OnPlayerJoined?.Invoke(playerInput);
                }
            }
        }
    }
}