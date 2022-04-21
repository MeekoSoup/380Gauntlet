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
        public int healthMod;
        public int scoreMod;
        public bool isNuke;
        public bool isKey;
        
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