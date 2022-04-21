using Data;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class PickupManager : MonoBehaviour
    {
        public EventNetwork eventNetwork;
        public PlayerData playerData;

        private void OnTriggerEnter(Collider other)
        {
            var pickup = other.GetComponent<Pickup>();
            playerData.health += pickup.healthMod;
            playerData.score += pickup.scoreMod;
            if (pickup.isKey) playerData.keys++;
            if (pickup.isNuke) eventNetwork.OnPlayerPickupNuke?.Invoke();
            pickup.Release();
        }
    }
}