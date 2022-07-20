using UnityEngine;

namespace Code.Enemies
{
    public class HeavyBanditMovement : MonoBehaviour
    {
        [SerializeField] 
        private HeavyBanditAnimator _heavyBanditAnimator;
        
        [SerializeField] 
        private Rigidbody2D _rigidbody2D;
        
        [SerializeField] 
        private float _speed;
        
        public void Stand(bool flipX) => 
            _heavyBanditAnimator.Stand(flipX);

        public void Move(float horizontalDirection)
        {
            var direction = new Vector2(horizontalDirection, 0);
            
            _rigidbody2D.position += direction * _speed;
            _heavyBanditAnimator.Move(horizontalDirection);
        }
    }
}