using System.Collections;
using UnityEngine;

namespace Code.Heroes
{
    public class HeroMovement : MonoBehaviour
    {
        private const string HorizontalAxis = "Horizontal";
        
        [SerializeField] 
        private Rigidbody2D _rigidbody2D;

        [SerializeField] 
        private HeroAnimator _heroAnimator;
        
        [SerializeField] 
        private float _speed;
        
        [SerializeField]
        private float _speedMoveDown;
        
        [SerializeField] 
        private float _jumpForce;

        [SerializeField]
        private float _rollSpeed;
        
        [SerializeField]
        private float _rollDelay;

        [SerializeField] 
        private AttackData[] _attacksData;

        [SerializeField] 
        private HeroAttack _heroAttack;

        [SerializeField] 
        private Vector2 _offsetAttack;
        
        private float _horizontalDirection;
        private bool _isGrounded;
        private bool _isRoll;
        private bool _isJump;
        private bool _isAttack;
        private bool _isAttackRecharge;

        public void TryMove()
        {
            if (!_isRoll && !_isAttack)
                Move();
            else
                Stand();
        }

        public void TryMoveDown()
        {
            if (!_isRoll && !_isAttack)
                MoveDown();
        }

        public void TryDirectionUpdate()
        {
            if (!_isRoll && !_isAttack)
                DirectionUpdate();
        }

        public void TryAttack(int damage, Hero hero)
        {
            if (!_isRoll && !_isAttackRecharge && _isGrounded)
                StartCoroutine(Attack(damage));
        }

        public void TryRoll()
        {
            if (!_isRoll && _isGrounded && !_isAttack)
                StartCoroutine(Roll());
        }

        public void TryJump()
        {
            if (!_isRoll && !_isJump && !_isAttack)
                Jump();
        }

        private void DirectionUpdate() => 
            _horizontalDirection = Input.GetAxisRaw(HorizontalAxis);

        public void Stand() => 
            _heroAnimator.Stand();

        private IEnumerator Roll()
        {
            _isRoll = true;
            _rigidbody2D.AddForce(Vector2.right * _rollSpeed * _horizontalDirection, ForceMode2D.Impulse);
            _heroAnimator.Roll();
            yield return new WaitForSeconds(_rollDelay);
            _isRoll = false;
            _rigidbody2D.velocity = Vector2.zero;
        }
        
        private IEnumerator Attack(int damage)
        {
            var numberAttack = Random.Range(0, _attacksData.Length);
            var offset = new Vector3(_offsetAttack.x * _horizontalDirection, _offsetAttack.y, 0);
            
            _heroAnimator.Attack(numberAttack);
            yield return new WaitForSeconds(_attacksData[numberAttack].BeforeDelay);
            var attack = Instantiate(_heroAttack, transform.position + offset, Quaternion.identity);
            attack.Init(damage);
            
            _isAttack = true;
            _isAttackRecharge = true;
            _rigidbody2D.AddForce(Vector2.right * _attacksData[numberAttack].Speed * _horizontalDirection, ForceMode2D.Impulse);
            yield return new WaitForSeconds(_attacksData[numberAttack].Delay);
            
            if (attack)
                Destroy(attack.gameObject);
            
            _isAttack = false;
            _rigidbody2D.velocity = Vector2.zero;
            
            yield return new WaitForSeconds(_attacksData[numberAttack].Recharge);
            _isAttackRecharge = false;
        }

        private void Jump()
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _heroAnimator.Jump();
            _isJump = true;
        }

        private void Move()
        {
            var direction = new Vector2(_horizontalDirection, 0);
            
            _rigidbody2D.position += direction * _speed;
            _heroAnimator.Move(_horizontalDirection);
        }
        
        private void MoveDown()
        {
            var direction = new Vector2(0, -1);
            _rigidbody2D.position += direction * _speedMoveDown;
        }

        public void Landing()
        {
            _isGrounded = true;
            _isJump = false;
            _heroAnimator.Landing();
        }

        public void Fall()
        {
            _isGrounded = false;
            _heroAnimator.Fall();
        }
    }
}