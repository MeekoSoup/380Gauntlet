using System;
using System.Collections;
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
        public TMP_Text heroNameText;
        public Image heroPortraitImage;
        public TMP_Text healthText;
        public TMP_Text keysText;
        public TMP_Text potionsText;
        public TMP_Text treasureText;

        [Header("Inventory Properties")]
        public GameObject potionPrefab;
        
        private IWeapon _weapon;
        private PlayerInput _playerInput;

        // private bool _coinStarted;

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
            eventNetwork.OnPlayerDamaged += CheckHealth;
            eventNetwork.OnPlayerDeathChant += CheckHealth;
            eventNetwork.OnPlayerPickup += UpdateGUI;
            eventNetwork.OnPlayerUseNuke += UpdateGUI;
            eventNetwork.OnPlayerDeathChant += UpdateGUI;
        }

        private void OnDisable()
        {
            eventNetwork.OnPlayerDamaged -= UpdateGUI;
            eventNetwork.OnPlayerDamaged -= CheckHealth;
            eventNetwork.OnPlayerDeathChant -= CheckHealth;
            eventNetwork.OnPlayerPickup -= UpdateGUI;
            eventNetwork.OnPlayerUseNuke -= UpdateGUI;
            eventNetwork.OnPlayerDeathChant -= UpdateGUI;
        }

        public void Attack()
        {
            if (!weaponParent) return;
            // Debug.Log($"playerData: {playerData}");
            // if (playerData.health <= 0) return;
            _weapon ??= _weapon = GetComponentInChildren<IWeapon>();
            _weapon?.Attack();
        }

        private void CheckHealth(PlayerInput playerInput = null)
        {
            if (playerData.health <= 0)
            {
                eventNetwork.OnPlayerKilled?.Invoke(_playerInput);
                PlayerManager.Instance.UnregisterPlayer(_playerInput);
                UpdateGUI();
                GetComponent<PlayerMovement>().enabled = false;
                graphicsParent.SetActive(false);
                weaponParent.SetActive(false);
                playerData.Reset();
                // TODO: Remove only if you can properly remove reliance on finding this class from other objects
                this.enabled = false;
                // gameObject.SetActive(false);
            }
        }

        public void UsePotion()
        {
            if (!playerData) return;
            if (playerData.potions <= 0) return;
            if (playerData.health <= 0) return;
            playerData.potions--;
            eventNetwork.OnPlayerUseNuke?.Invoke(_playerInput);
        }

        private void UpdateGUI(PlayerInput playerInput = null)
        {
            if (heroNameText)
                heroNameText.text = playerData.heroName;
            if (heroPortraitImage)
                heroPortraitImage.sprite = playerData.heroPortrait;
            if (healthBarImage)
                healthBarImage.fillAmount = Mathf.InverseLerp(0, playerData.healthStarting, playerData.health);
            if (healthText)
            {
                healthText.text = playerData.health.ToString();
                if (playerData.health <= 0)
                {
                    healthText.text = "DEAD";
                    // if (!_coinStarted)
                    // {
                    //     StartCoroutine(InsertCoin());
                    // }
                }
            }
            if (keysText)
                keysText.text = playerData.keys.ToString();
            if (potionsText)
                potionsText.text = playerData.potions.ToString();
            if (treasureText)
                treasureText.text = playerData.score.ToString();
        }

        // private IEnumerator InsertCoin()
        // {
        //     _coinStarted = true;
        //     var count = 0;
        //     while (true)
        //     {
        //         if (healthText) healthText.text = "DEAD";
        //         UpdateGUI();
        //         yield return new WaitForSeconds(1f);
        //         if (healthText) healthText.text = "INSERT COIN";
        //         UpdateGUI();
        //         count++;
        //         if (count >= 10) StopCoroutine(InsertCoin());
        //     }
        // }
    }
}