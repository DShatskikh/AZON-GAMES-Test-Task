using Code.Heroes;
using UnityEngine;

namespace Code.Enemies
{
    public class HeavyBanditAttack : MonoBehaviour
    {
        private int _damage;

        public void Init(int damage)
        {
            _damage = damage;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Hero hero)) 
                hero.Damage(_damage);
        }
    }
}