using System;
using Camera;
using Control;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Character
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerOverseer : MonoBehaviour
    {
        public EventNetwork eventNetwork;
        public PlayerRole role = PlayerRole.None;
        public PlayerData playerData;
        public GameObject followCam;
        public GameObject graphicsParent;
        public GameObject weaponParent;

        [Header("GUI Properties")] 
        public Image healthBarImage;
        public TMP_Text healthText;
        public TMP_Text keysText;
        public TMP_Text potionsText;
        public TMP_Text treasureText;

        private IWeapon _weapon;
        private PlayerInput _playerInput;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        private void Start()
        {
            PlayerManager.Instance.RegisterPlayer(_playerInput);
            eventNetwork.OnPlayerJoined?.Invoke(_playerInput);
            UpdateGUI();

        }

        private void OnEnable()
        {
            eventNetwork.OnPlayerDamaged += UpdateGUI;
            eventNetwork.OnPlayerPickup += UpdateGUI;
            eventNetwork.OnPlayerUseNuke += UpdateGUI;
        }

        private void OnDisable()
        {
            eventNetwork.OnPlayerDamaged -= UpdateGUI;
            eventNetwork.OnPlayerPickup -= UpdateGUI;
            eventNetwork.OnPlayerUseNuke -= UpdateGUI;
        }

        public void Attack()
        {
            if (!weaponParent) return;
            _weapon ??= _weapon = GetComponentInChildren<IWeapon>();
            _weapon?.Attack();
        }

        public void UsePotion()
        {
            if (!playerData) return;
            if (playerData.potions <= 0) return;
            playerData.potions--;
            eventNetwork.OnPlayerUseNuke?.Invoke(_playerInput);
        }

        private void UpdateGUI(PlayerInput playerInput = null)
        {
            if (healthBarImage)
                healthBarImage.fillAmount = Mathf.InverseLerp(0, playerData.healthStarting, playerData.health);
            if (healthText)
                healthText.text = playerData.health.ToString();
            if (keysText)
                keysText.text = playerData.keys.ToString();
            if (potionsText)
                potionsText.text = playerData.potions.ToString();
            if (treasureText)
                treasureText.text = playerData.score.ToString();
        }
    }
}