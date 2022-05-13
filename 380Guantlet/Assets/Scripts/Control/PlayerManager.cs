using System;
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
        public PlayerData elf;
        public PlayerData valkyrie;
        public PlayerData warrior;
        public PlayerData wizard;
        private readonly List<PlayerInput> _playerInputs = new List<PlayerInput>();
        private List<PlayerRole> _remainingRoles = new List<PlayerRole>();

        public override void Awake()
        {
            _remainingRoles.Add(PlayerRole.Elf);
            _remainingRoles.Add(PlayerRole.Warrior);
            _remainingRoles.Add(PlayerRole.Wizard);
            _remainingRoles.Add(PlayerRole.Valkyrie);

            ResetPlayerData();
        }

        private void ResetPlayerData()
        {
            elf.Reset();
            valkyrie.Reset();
            warrior.Reset();
            wizard.Reset();
        }

        public void RegisterPlayer(PlayerInput playerInput)
        {
            if (!_playerInputs.Contains(playerInput))
            {
                _playerInputs.Add(playerInput);
                InstructionRemover.SetPlayerCount(_playerInputs.Count);
                ChooseRole(playerInput);
                MoveNearOtherPlayers(playerInput);
            }
        }

        public void UnregisterPlayer(PlayerInput playerInput)
        {
            if (_playerInputs.Contains(playerInput))
            {
                PlayerRole role = GetPlayerRole(playerInput);
                _playerInputs.Remove(playerInput);
                _remainingRoles.Add(role);
            }
        }

        private void MoveNearOtherPlayers(PlayerInput playerInput)
        {
            var go = playerInput.gameObject;
            Bounds bounds = new Bounds();
            foreach (var input in _playerInputs)
            {
                bounds.Encapsulate(input.transform.position);
            }

            var pos = playerInput.transform.position;
            playerInput.transform.position = new Vector3(bounds.center.x, pos.y, bounds.center.y);
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
            overseer.role = _remainingRoles[r];
            _remainingRoles.RemoveAt(r);
            
            switch (overseer.role)
            {
                case PlayerRole.None:
                    break;
                case PlayerRole.Elf:
                    overseer.playerData = elf;
                    overseer.gameObject.AddComponent<Questor>();
                    break;
                case PlayerRole.Valkyrie:
                    overseer.playerData = valkyrie;
                    overseer.gameObject.AddComponent<Thyra>();
                    break;
                case PlayerRole.Warrior:
                    overseer.playerData = warrior;
                    overseer.gameObject.AddComponent<Thor>();
                    break;
                case PlayerRole.Wizard:
                    overseer.playerData = wizard;
                    overseer.gameObject.AddComponent<Merlin>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var playerData = overseer.playerData;
            // setup graphics and weapon
            if (overseer.graphicsParent && playerData.heroGraphics)
                Instantiate(playerData.heroGraphics, overseer.graphicsParent.transform, false);

            if (overseer.weaponParent && playerData.heroWeapon)
                Instantiate(playerData.heroWeapon, overseer.weaponParent.transform, false);

            var playerMovement = overseer.GetComponent<PlayerMovement>();
            playerMovement.speed = playerData.speed;

            overseer.gameObject.name = overseer.playerData.heroName;
        }

        public GameObject GetNearestPlayer(Transform origin)
        {
            GameObject closestPlayer = null;
            float shortestDist = float.MaxValue;
            float dist = 0;

            foreach (var playerInput in _playerInputs)
            {
                dist = Vector3.Distance(origin.position, playerInput.transform.position);
                if (dist < shortestDist)
                {
                    shortestDist = dist;
                    closestPlayer = playerInput.gameObject;
                }
            }

            return closestPlayer;
        }
    }
}