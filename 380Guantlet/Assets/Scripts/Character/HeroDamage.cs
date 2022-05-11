using System;
using UnityEngine;

namespace Character
{
    public class HeroDamage : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var baseEnemy = other.gameObject.GetComponent<BaseEnemy>();
            if (!baseEnemy) return;
            baseEnemy.TakeDamage();
        }
    }
}