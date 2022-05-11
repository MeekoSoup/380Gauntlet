using System;
using Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    /**
     * PickupTestClient is just a test script for testing the interaction between Pickups and the PickupManager.
     *
     * DO NOT USE IN PRODUCTION
     *
     * @author: Weston R. Campbell 
     */
    public class PickupTestClient : MonoBehaviour
    {
        public EventNetwork eventNetwork;
        public PlayerData playerData;
        private PickupManager _pickupManager;

        private void Awake()
        {
            _pickupManager = gameObject.AddComponent<PickupManager>();
            _pickupManager.eventNetwork = eventNetwork;
            _pickupManager.playerData = playerData;
        }

        private void OnEnable()
        {
            eventNetwork.OnPlayerPickupNuke += NukeTest;
        }

        private void OnDisable()
        {
            eventNetwork.OnPlayerPickupNuke -= NukeTest;
        }

        private void OnGUI()
        {
            GUI.color = Color.red;
            GUI.Label(new Rect(0, 0, 500, 60), $"Health: {playerData.health}");
            GUI.color = Color.yellow;
            GUI.Label(new Rect(0, 20, 500, 60), $"Keys: {playerData.keys}");
            GUI.color = Color.white;
            GUI.Label(new Rect(0, 40, 500, 60), $"Score: {playerData.score}");
        }

        private void NukeTest(PlayerInput playerInput = null)
        {
            Debug.Log("Nuclear launch detected!");
        }
    }
}