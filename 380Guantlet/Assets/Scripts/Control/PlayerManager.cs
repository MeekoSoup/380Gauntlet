using System.Collections.Generic;
using Character;
using Data;
using General;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Control
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        public EventNetwork eventNetwork;
        private readonly List<PlayerInput> _playerInputs = new List<PlayerInput>();
        private List<PlayerRole> _remainingRoles = new List<PlayerRole>();

        public override void Awake()
        {
            _remainingRoles.Add(PlayerRole.Elf);
            _remainingRoles.Add(PlayerRole.Fighter);
            _remainingRoles.Add(PlayerRole.Rogue);
            _remainingRoles.Add(PlayerRole.Valkyrie);
        }

        public void RegisterPlayer(PlayerInput playerInput)
        {
            if (!_playerInputs.Contains(playerInput))
            {
                _playerInputs.Add(playerInput);
                ChooseRole(playerInput);
            }
        }

        public void UnregisterPlayer(PlayerInput playerInput)
        {
            if (_playerInputs.Contains(playerInput))
            {
                PlayerRole role = GetPlayerRole(playerInput);
                _playerInputs.Remove(playerInput);
            }
        }

        private PlayerRole GetPlayerRole(PlayerInput playerInput)
        {
            PlayerOverseer overseer = playerInput.gameObject.GetComponent<PlayerOverseer>();
            return overseer.role;
        }

        private void ChooseRole(PlayerInput playerInput)
        {
            PlayerOverseer overseer = playerInput.gameObject.GetComponent<PlayerOverseer>();
            int r = Random.Range(0, _remainingRoles.Count);
            switch (r)
            {
                case 0:
                    Debug.Log("Fighter");
                    overseer.role = PlayerRole.Fighter;
                    break;
                case 1:
                    Debug.Log("Valkyrie");
                    overseer.role = PlayerRole.Valkyrie;
                    break;
                case 2:
                    Debug.Log("Elf");
                    overseer.role = PlayerRole.Elf;
                    break;
                case 3:
                    Debug.Log("Rogue");
                    overseer.role = PlayerRole.Rogue;
                    break;
                default:
                    break;
            }
            _remainingRoles.RemoveAt(r);
        }
    }
}