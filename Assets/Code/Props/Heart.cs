using Code.Heroes;
using UnityEngine;

namespace Code.Props
{
    public class Heart : MonoBehaviour
    {
        private int _health = 1;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Hero hero))
            {
                hero.Treatment(_health);
                Destroy(gameObject);
            }
        }
    }
}