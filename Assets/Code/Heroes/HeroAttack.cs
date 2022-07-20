using Code.Enemies;
using UnityEngine;

namespace Code.Heroes
{
    public class HeroAttack : MonoBehaviour
    {
        private int _damage;

        public void Init(int damage)
        {
            _damage = damage;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out HeavyBandit heavyBandit)) 
                heavyBandit.Damage(_damage);
        }
    }
}