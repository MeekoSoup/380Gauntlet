using UnityEngine;

namespace Character
{
    public class Weapon : MonoBehaviour
    {
        public float attackCooldown = 0.5f;
        private Animator _animator;
        private float _cooldownClock;
        private static readonly int Swing1 = Animator.StringToHash("Swing");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _cooldownClock = attackCooldown;
        }

        private void Update()
        {
            if (_cooldownClock > 0)
                _cooldownClock -= Time.deltaTime;
        }

        public void Swing()
        {
            if (!_animator) return;
            if (_cooldownClock > 0) return;
            _animator.SetBool(Swing1, true);
            _cooldownClock = attackCooldown;
        }

        public void ResetSwing()
        {
            if (!_animator) return;
            _animator.SetBool(Swing1, false);
        }
    }
}