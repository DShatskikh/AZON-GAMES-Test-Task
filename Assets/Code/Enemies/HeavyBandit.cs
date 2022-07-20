using System;
using System.Collections;
using Code.Heroes;
using Code.Infrastructure;
using Code.Props;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Enemies
{
    public class HeavyBandit : MonoBehaviour
    {
        [SerializeField] 
        private HeavyBanditMovement _heavyBanditMovement;

        [SerializeField] 
        private HeavyBanditAnimator _heavyBanditAnimator;

        [SerializeField] 
        private Rigidbody2D _rigidbody2D;

        [SerializeField] 
        private Slider _healthSlider;

        [SerializeField] 
        private Heart _heart;

        [SerializeField]
        private float _invulnerabilityDelay;

        [SerializeField] 
        private float _allowance;

        [SerializeField]
        private HeavyBanditAttack _attack;

        [SerializeField]
        private AttackData _attackData;

        [SerializeField] 
        private Vector2 _offsetAttack;
        
        private SessionDataService _sessionDataService;
        private Hero _hero;
        private int _health = 1;
        private int _damage = 1;
        private float _horizontalDirection;
        private float _maxVertacalAttackDestance = 0.5f;
        private bool _isDead;
        private bool _isInvulnerability;
        private bool _isAttack;
        private bool _isAttackRecharge;

        public void Init(SessionDataService sessionDataService, Hero hero, int health)
        {
            _sessionDataService = sessionDataService;
            _hero = hero;
            _health = health;
            _healthSlider.maxValue = health;
            _healthSlider.value = health;
        }

        private void FixedUpdate()
        {
            if (_isDead || _isAttack)
                return;
            
            TryMove();
            TryHorizontalDirection();
        }

        public void Damage(int damage)
        {
            if (_isInvulnerability || _isDead)
                return;

            _health -= damage;

            if (_health > 0)
                _healthSlider.value = _health;
            else
            {
                _healthSlider.value = 0;
                _healthSlider.gameObject.SetActive(false);
            }
            
            if (_health <= 0)
                Dead();
            else
                _heavyBanditAnimator.Damage();
            
            StartCoroutine(RecoveryAfterDamage());
        }

        private void TryHorizontalDirection()
        {
            if (Math.Abs(_hero.transform.position.x - transform.position.x) > 0.0001f)
                _horizontalDirection = _hero.transform.position.x > transform.position.x ? 1 : -1;
        }

        private void TryMove()
        {
            if (_hero.transform.position.x > transform.position.x + _allowance)
                _heavyBanditMovement.Move(1);
            else if (_hero.transform.position.x < transform.position.x - _allowance)
                _heavyBanditMovement.Move(-1);
            else
            {
                _heavyBanditMovement.Stand(_hero.transform.position.x > transform.position.x);

                if (!_isAttackRecharge && !_hero.IsDead &&
                    _hero.transform.position.y < transform.position.y + _maxVertacalAttackDestance)
                    StartCoroutine(Attack(_damage));
            }
        }

        private IEnumerator Attack(int damage)
        {
            var offset = new Vector3(_offsetAttack.x * _horizontalDirection, _offsetAttack.y, 0);
            
            _heavyBanditAnimator.Attack();
            yield return new WaitForSeconds(_attackData.BeforeDelay);
            var attack = Instantiate(_attack, transform.position + offset, Quaternion.identity);
            attack.Init(damage);
            
            _isAttack = true;
            _isAttackRecharge = true;
            _rigidbody2D.AddForce(Vector2.right * _attackData.Speed * _horizontalDirection, ForceMode2D.Impulse);
            yield return new WaitForSeconds(_attackData.Delay);
            
            if (_isDead)
                _heavyBanditAnimator.Dead();
            
            if (attack)
                Destroy(attack.gameObject);
            
            _isAttack = false;
            _rigidbody2D.velocity = Vector2.zero;
            yield return new WaitForSeconds(_attackData.Recharge);
            _isAttackRecharge = false;
        }

        private IEnumerator RecoveryAfterDamage()
        {
            _isInvulnerability = true;
            yield return new WaitForSeconds(_invulnerabilityDelay);
            _isInvulnerability = false;
        }

        private void Dead()
        {
            _isDead = true;
            _sessionDataService.AddScore();
            _heavyBanditAnimator.Dead();
            Instantiate(_heart, transform.position, quaternion.identity);
        }
    }
}