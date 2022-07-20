using System.Collections;
using Code.Spawners;
using Code.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Heroes
{
    public class Hero : MonoBehaviour
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";
        
        [SerializeField] 
        private HeroAnimator _heroAnimator;
        
        [SerializeField]
        private HeroMovement _heroMovement;

        [SerializeField] 
        private Slider _healthSlider;

        [SerializeField] 
        private EndWindow _endWindow;

        [SerializeField] 
        private EntitySpawner _entitySpawner;
        
        [SerializeField]
        private float _invulnerabilityDelay;

        private int _health = 20;
        private int _damage = 1;
        private bool _isDead;
        private bool _isInvulnerability;

        public bool IsDead => _isDead;

        public void Init()
        {
            _healthSlider.maxValue = _health;
            HealthSliderUpdate();
        }

        private void Update()
        {
            Inputting();
        }

        private void FixedUpdate()
        {
            if (_isDead)
                return;
            
            if (Input.GetAxisRaw(HorizontalAxis) != 0)
                _heroMovement.TryMove();
            else
                _heroMovement.Stand();

            if (Input.GetAxisRaw(VerticalAxis) < 0)
                _heroMovement.TryMoveDown();
        }

        public void Damage(int damage)
        {
            if (_isInvulnerability || _isDead)
                return;
            
            _health -= damage;
            _healthSlider.value = _health;
            
            if (_health <= 0)
                StartCoroutine(Dead());
            
            StartCoroutine(RecoveryAfterDamage());
        }

        public void Treatment(int health)
        {
            if (health <= 0)
                Debug.LogError("Переданно отрицательное здоровье");
            
            _health += health;
            
            if (_health > _healthSlider.maxValue)
                _healthSlider.maxValue = _health;
            
            HealthSliderUpdate();
        }

        private IEnumerator RecoveryAfterDamage()
        {
            _isInvulnerability = true;
            yield return new WaitForSeconds(_invulnerabilityDelay);
            _isInvulnerability = false;
        }
        
        private void Inputting()
        {
            if (_isDead)
                return;
            
            if (Input.GetKeyDown(KeyCode.Space))
                _heroMovement.TryJump();
            
            if (Input.GetKeyDown(KeyCode.LeftShift))
                _heroMovement.TryRoll();
            
            if (Input.GetMouseButtonDown(0))
                _heroMovement.TryAttack(_damage, this);
            
            if (Input.GetAxisRaw(HorizontalAxis) != 0)
                _heroMovement.TryDirectionUpdate();
        }

        private IEnumerator Dead()
        {
            _heroMovement.enabled = false;
            _heroAnimator.Dead();
            _isDead = true;
            yield return new WaitForSeconds(1);
            _endWindow.gameObject.SetActive(true);
            _entitySpawner.StopSpawn();
        }

        private void HealthSliderUpdate() => 
            _healthSlider.value = _health;
    }
}