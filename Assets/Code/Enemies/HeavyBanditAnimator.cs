using UnityEngine;

namespace Code.Enemies
{
    public class HeavyBanditAnimator : MonoBehaviour
    {
        private static readonly int DeadHash = Animator.StringToHash("Die");
        private static readonly int DamageHash = Animator.StringToHash("Damage");
        private static readonly int AttackHash = Animator.StringToHash("Attack");
        private static readonly int HorizontalHash = Animator.StringToHash("Horizontal");

        [SerializeField] 
        private Animator _animator;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        public void Move(float direction)
        {
            _spriteRenderer.flipX = direction > 0;
            _animator.SetFloat(HorizontalHash, 1);
        }

        public void Stand(bool flipX)
        {
            _spriteRenderer.flipX = flipX;
            _animator.SetFloat(HorizontalHash, 0);
        }

        public void Attack() => 
            _animator.SetTrigger(AttackHash);
        
        public void Dead() => 
            _animator.SetTrigger(DeadHash);

        public void Damage() => 
            _animator.SetTrigger(DamageHash);
    }
}