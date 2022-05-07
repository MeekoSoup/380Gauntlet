using Data;
using UnityEngine;

namespace Character
{
    /**
     * PickupManager contains logic for what to do when this object collides with a Pickup object.
     *  The OnPlayerPickup event will be invoked if a Pickup is collided with. If that Pickup is
     *  a nuke/potion, then it will invoke the OnPlayerPickupNuke event.
     *
     * @author: Weston R. Campbell
     */
    
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class PickupManager : MonoBehaviour
    {
        public EventNetwork eventNetwork;
        public PlayerData playerData;

        private void OnTriggerEnter(Collider other)
        {
            var pickup = other.GetComponent<Pickup>();
            if (!pickup) return;
            
            playerData.health += pickup.healthMod;
            playerData.score += pickup.scoreMod;
            if (pickup.isKey) playerData.keys++;
            if (pickup.isNuke) eventNetwork.OnPlayerPickupNuke?.Invoke();
            eventNetwork.OnPlayerPickup?.Invoke();
            pickup.Release();
        }
    }
}