using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class MultiplayerManager : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
        private GameObject _lastActivePlayer;
        private PlayerInputManager _inputManager;

        private void Awake()
        {
            _inputManager = GetComponent<PlayerInputManager>();
        }

        public void TrySpawnPlayer()
        {
            Debug.Log("Player joined!");
        }
    }
}