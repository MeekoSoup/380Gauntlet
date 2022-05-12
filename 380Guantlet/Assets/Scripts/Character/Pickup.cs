using System;
using Data;
using UnityEngine;

namespace Character
{
    /**
     * Pickup is a very simple component that just holds data for use
     *  with the PickupManager.
     *
     * @author: Weston R. Campbell
     */
    public class Pickup : MonoBehaviour
    {
        public EventNetwork eventNetwork;
        public int healthMod;
        public int scoreMod;
        public bool isNuke;
        public bool isKey;

        private void OnTriggerEnter(Collider other)
        {
            // if (!other.transform.parent) return;
            // var parent = other.transform.parent;
            // Debug.Log(parent.gameObject.name);
            PlayerOverseer po = other.GetComponent<PlayerOverseer>();
            if (po)
            {
                var pd = po.playerData;
                pd.health += healthMod;
                pd.score += scoreMod;
                pd.keys += (isKey) ? 1 : 0;
                pd.potions += (isNuke) ? 1 : 0;
                eventNetwork.OnPlayerPickup?.Invoke();
                Release();
            }
        }

        /**
         * Release is used when the pickup is picked up. By default it just 
         *  destroys the GameObject.
         */
        public void Release()
        {
            Destroy(gameObject);
        }
    }
}