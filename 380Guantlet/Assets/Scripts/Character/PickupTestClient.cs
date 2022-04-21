using System;
using System.Reflection.Emit;
using Data;
using UnityEngine;

namespace Character
{
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

        private void OnGUI()
        {
            GUI.color = Color.red;
            GUI.Label(new Rect(0, 0, 500, 60), $"Health: {playerData.health}");
            GUI.color = Color.yellow;
            GUI.Label(new Rect(0, 20, 500, 60), $"Keys: {playerData.keys}");
            GUI.color = Color.white;
            GUI.Label(new Rect(0, 40, 500, 60), $"Score: {playerData.score}");
        }

        private void NukeTest(PlayerData pd = null)
        {
            Debug.Log("Nuclear launch detected!");
        }
    }
}