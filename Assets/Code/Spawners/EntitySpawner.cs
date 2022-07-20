using System.Collections;
using Code.Enemies;
using Code.Heroes;
using Code.Infrastructure;
using UnityEngine;

namespace Code.Spawners
{
    public class EntitySpawner : MonoBehaviour
    {
        [SerializeField] 
        private HeavyBandit _heavyBandit;

        [SerializeField] 
        private Transform _container;

        [SerializeField]
        private Transform _targetRight;
        
        [SerializeField] 
        private Transform _targetLeft;

        private SessionDataService _sessionDataService;
        private Hero _hero;
        private float _delay = 4f;

        public void Init(SessionDataService sessionDataService, Hero hero)
        {
            _sessionDataService = sessionDataService;
            _hero = hero;
        }

        public void StartSpawn() => 
            StartCoroutine(Spawn());

        public void StopSpawn() => 
            StopCoroutine(Spawn());

        private IEnumerator Spawn()
        {
            yield return new WaitForSeconds(1);
            var counter = 0;
            
            while (true)
            {
                var position = counter % 2 == 1 ? _targetRight.position : _targetLeft.position;
                var heavyBandit = Instantiate(_heavyBandit, position, Quaternion.identity, _container);
                var damage = 1 + counter / 10;
                heavyBandit.Init(_sessionDataService, _hero, damage);
                yield return new WaitForSeconds(_delay);
                ++counter;
            }
        }
    }
}