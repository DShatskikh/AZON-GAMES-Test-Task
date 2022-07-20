using UnityEngine;

namespace Code.Heroes
{
    public class HeroGroundCheck : MonoBehaviour
    {
        [SerializeField] 
        private HeroMovement _heroMovement;
        
        private void OnTriggerStay2D(Collider2D other) => 
            _heroMovement.Landing();

        private void OnTriggerExit2D(Collider2D other) => 
            _heroMovement.Fall();
    }
}