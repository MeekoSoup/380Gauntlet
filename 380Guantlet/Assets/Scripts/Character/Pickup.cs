using UnityEngine;

namespace Character
{
    public class Pickup : MonoBehaviour
    {
        public int healthMod;
        public int scoreMod;
        public bool isNuke;
        public bool isKey;

        public void Release()
        {
            Destroy(gameObject);
        }
    }
}