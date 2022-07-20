using UnityEngine;

namespace Code.Heroes
{
    public class HeroAnimator : MonoBehaviour
    {
        private static readonly int RollHash = Animator.StringToHash("Roll");
        private static readonly int JumpHash = Animator.StringToHash("Jump");
        private static readonly int HorizontalHash = Animator.StringToHash("Horizontal");
        private static readonly int FallHash = Animator.StringToHash("Fall");
        private static readonly int AttackHash = Animator.StringToHash("Attack");
        private static readonly int NumberAttackHash = Animator.StringToHash("AttackNumber");
        private static readonly int DeadHash = Animator.StringToHash("Die");

        [SerializeField] 
        private Animator _animator;

        [SerializeField] 
        private SpriteRenderer _spriteRenderer;

        public void Roll() => 
            _animator.SetTrigger(RollHash);

        public void Jump() => 
            _animator.SetTrigger(JumpHash);

        public void Move(float direction)
        {
            _spriteRenderer.flipX = direction < 0;
            _animator.SetFloat(HorizontalHash, 1);
        }

        public void Stand() => 
            _animator.SetFloat(HorizontalHash, 0);

        public void Fall() => 
            _animator.SetBool(FallHash, true);

        public void Landing() => 
            _animator.SetBool(FallHash, false);

        public void Attack(int numberAttack)
        {
            _animator.SetTrigger(AttackHash);
            _animator.SetFloat(NumberAttackHash, numberAttack);
        }

        public void Dead() => 
            _animator.SetTrigger(DeadHash);
    }
}