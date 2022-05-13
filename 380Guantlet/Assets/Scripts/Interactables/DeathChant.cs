using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Character;
using Data;
using UnityEngine;

namespace Interactables
{
    /**
     * DeathChant periodically reduces the health for all the players. I don't know why I named it this. :\
     */
    public class DeathChant : MonoBehaviour
    {
        public EventNetwork eventNetwork;
        
        [Min(0.01f), Tooltip("Amount of health drained each cycle.")]
        public float healthDrainAmount = 1f;
        
        [Range(0.01f, 10f), Tooltip("Time between health drains.")]
        public float cycleInterval = 2f;
        
        private List<PlayerOverseer> _players = new List<PlayerOverseer>();

        private void OnEnable()
        {
            StartCoroutine(Chant());
        }

        private void OnDisable()
        {
            StopCoroutine(Chant());
        }

        private IEnumerator Chant()
        {
            while (true)
            {
                _players = GameObject.FindObjectsOfType<PlayerOverseer>().ToList();
                foreach (var player in _players)
                {
                    player.playerData.health -= healthDrainAmount;
                }

                eventNetwork.OnPlayerDeathChant?.Invoke();
                yield return new WaitForSeconds(cycleInterval);
            }
        }
    }
}