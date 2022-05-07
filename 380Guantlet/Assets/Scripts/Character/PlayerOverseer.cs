using System;
using Control;
using Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerOverseer : MonoBehaviour
    {
        public EventNetwork eventNetwork;
        public PlayerRole role = PlayerRole.None;
        private PlayerInput _playerInput;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        private void Start()
        {
            Debug.Log($"{gameObject.name} has device ID: {_playerInput.devices[0].deviceId.ToString()}!");
            PlayerManager.Instance.RegisterPlayer(_playerInput);
        }
    }
}